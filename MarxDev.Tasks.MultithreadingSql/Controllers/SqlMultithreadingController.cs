

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MarxDev.Tasks.MultithreadingSql
{
    public class SqlMultithreadingController
    {
        /// <summary>
        /// Gijų stabdymo žetonas (berots:) 
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Gamykla duomenų rašymui į DB
        /// </summary>
        private readonly StringGenerationFabric _dbFabrik;

        /// <summary>
        /// Gijų skaičius
        /// </summary>
        public int ThreadQuantity { get; private set; }

        /// <summary>
        /// Generuojamų įrašų kiekis
        /// </summary>
        public int EntriesQuantity { get; private set; }

        public long GenerationElapsed { get;private set; } = 0;
        public int GenerationCount { get;private set; } = 0;

        /// <summary>
        /// Pagrindinis konstruktorius
        /// </summary>
        public SqlMultithreadingController(string connectionString)
        {
            _dbFabrik = new StringGenerationFabric(connectionString);
        }

        /// <summary>
        /// Perkuriam objektą iš naujo su reikiamom reikšmėm
        /// </summary>
        /// <param name="threadQuantity">Gijų skaičius > 0 </param>
        /// <param name="entriesQuantity">Generuojamų įrašų kiekis > 0 </param>
        public void Init(int threadQuantity, int entriesQuantity)
        {
            /// Netikrinu, nes tą dariau formoje (nors reikėtų)
            ThreadQuantity = threadQuantity;
            EntriesQuantity = entriesQuantity;
        }



        /// <summary>
        /// Gijų paleidimas
        /// </summary>
        /// <param name="progress">Objektas skirtas progreso grąžinimui</param>
        /// <returns>Visus sugeneruotus įrašus</returns>
        public async Task<IList<StringGeneration>> AssignAndRunTasksAsync(IProgress<int> progress)
        {
            try
            {
                //tiesiog įdomu kiek laiko užtruko :)
                Stopwatch stopWatch;
                stopWatch = Stopwatch.StartNew();

                //sukuriam gijų stabdymo šaltinį
                _cancellationTokenSource = new CancellationTokenSource();

                //sukuriam gijų masyvą (teko googlint kaip lietuviškai:)
                Task<IList<StringGeneration>>[] tasks = new Task<IList<StringGeneration>>[ThreadQuantity];

                //kad nereiketų lock'u ir semaforu, nusprendžiau tiesiog padalinti
                //generuojamu įrašų kiekius kiekvienai gijai

                //gaunam liekaną, kad vėliau padalinti gijoms
                var entriesReminder = EntriesQuantity % ThreadQuantity;
                //gaunam sveiką skaičių (minimalus generavimų kiekis gijai)
                var entriesPerThread = (EntriesQuantity - entriesReminder) / ThreadQuantity;


                for (int i = 0; i < ThreadQuantity; i++)
                {
                    var entriesToProcess = entriesPerThread;
                    //jeigu turim dar likutį
                    if (entriesReminder > 0)
                    {
                        //pridedam po vieną
                        entriesToProcess++;
                        //atimam iš likučio
                        entriesReminder--;
                    }
                    //paduodam DbContextOptions, kad kiekviena gija susikurtų savo DBContextą
                    var dbOptions = _dbFabrik.GetDbContextOptions;
                    //Kuriam darbus kiekvienai gijai
                    tasks[i] = Task.Run(() => FillEntries(entriesToProcess, dbOptions, progress, _cancellationTokenSource.Token));
                }

                //laukiam kol visos baigsis arba bus cancelintos
                await Task.WhenAll(tasks);

                //sukuriam konteineri įrašams iš gijų
                var allEntries = new List<StringGeneration>();

                //gaunam rezultatą iš visų gijų ir dedam į konteinerį
                for (int i = 0; i < ThreadQuantity; i++)
                {
                    IList<StringGeneration> currentList = await tasks[i];
                    allEntries.AddRange(currentList);
                }
                //Įrašom laiką ir kiek sugeneravom
                GenerationElapsed = stopWatch.ElapsedMilliseconds;
                stopWatch.Stop();
                GenerationCount = allEntries.Count;
                //parūšiuojam pagal generavimo tvarką maždauk
                return allEntries.OrderBy(a=>a.GenerationNumber).ThenBy(a=>a.ThreadId).ToList();
            }
            //Jeigu Exceptionas gijoje, reikia vis tiek Disposinti
            finally
            {
                //Naikinam stabdymo šaltinį
                _cancellationTokenSource.Dispose();
            }




        }

        /// <summary>
        /// Stabdom gijas
        /// </summary>
        public void CancelThreads()
        {
            if (!(_cancellationTokenSource is null))
            {
                _cancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Įrašų generavimas
        /// </summary>
        /// <param name="entriesQuantity">Įrašų kiekis</param>
        /// <param name="contextOptions">DbContexto sukūrimui gijos viduje</param>
        /// <param name="progress">Progreso callback'as</param>
        /// <param name="cancellationToken">Stabdymo žetonas</param>
        /// <returns>Sugeneruotus įrašus</returns>
        private Task<IList<StringGeneration>> FillEntries(
            int entriesQuantity,
            DbContextOptions<StringGenerationsContext> contextOptions,
            IProgress<int> progress,
            CancellationToken cancellationToken)
        {
            IList<StringGeneration> entriesInserted = new List<StringGeneration>();
            
            //Transaction'u nedariau, nutariau neišsiplėsti dar labiau
            using (var dbContext = new StringGenerationsContext(contextOptions))
            {
                //gaunam gijosID
                int threadId = Thread.CurrentThread.ManagedThreadId;

                //loopinam per įrašų kiekį reikalingą gijai
                for (int i = 0; i < entriesQuantity; i++)
                {
                   //Jeigu gija stabdoma
                    if (cancellationToken.IsCancellationRequested)
                    {
                        //gražinam ką spėjom
                        return Task.FromResult(entriesInserted);
                    }

                    //Saugojam įrašą, gaunam išsaugotą su ID
                    var currentEntrie = _dbFabrik.InsertStringGeneration(dbContext, GenerateEntrie(threadId,i));
                   
                    //įdedam į konteinerį
                    entriesInserted.Add(currentEntrie);
                    
                    //perduodam progressą
                    if (!(progress is null))
                    {
                        progress.Report(i);
                    }
                }

            }
            //gražinam viską
            return Task.FromResult(entriesInserted);
        }


        /// <summary>
        /// Sugeneruojamas StringGeneration be ID (ji gaus is DB)
        /// </summary>
        /// <param name="threadId">Gijos numeris</param>
        /// <returns>StringGeneration</returns>
        private static StringGeneration GenerateEntrie(int threadId,int generationNr) =>
             new StringGeneration { ThreadId = threadId, Text = GetRandomGeneratedString() ,GenerationNumber= generationNr };


        /// <summary>
        /// Sugeneruojamas atsitiktinis tekstas
        /// </summary>
        /// <returns>Sugeneruotas stringas</returns>
        private static string GetRandomGeneratedString()=>
            //nera tikslo sudetingai generacijai
            Convert.ToBase64String(Guid.NewGuid().ToByteArray());
       

    }
}
