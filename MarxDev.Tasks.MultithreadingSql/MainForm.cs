
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarxDev.Tasks.MultithreadingSql
{
    public partial class MainForm : Form
    {
        private readonly SqlMultithreadingController _controller;
        private IList<StringGeneration> _stringGenerations;

        public MainForm()
        {
            InitializeComponent();
            //inicijuojam kontrolerį
            string connectionString = ConfigurationManager.ConnectionStrings["StringGenerationsConnectionString"].ConnectionString;
            _controller = new SqlMultithreadingController(connectionString);


        }

        /// <summary>
        /// Atnaujinamas tuščias gridas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitGrid();
        }



        /// <summary>
        /// Validuojamas įrašų kiekio įvedimas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtBxEntriesQuantityKeyPress(object sender, KeyPressEventArgs e)
        {
            //Patikrinam, kad butu sveikas skaicius
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Įrašų generavimo ir gijų kūrimo mygtuko paspaudimas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnStartClick(object sender, EventArgs e)
        {
            try
            {
                //inicijuojam naujus kiekius iš formos
                _controller.Init(GetThreadQuantity(), GetEntriesQuantity());

                //paruošiam formą naujam generavimui
                ClearForm();

                WriteMessage($"Pradedam įrašų generavimą, įrašų kiekis: {_controller.EntriesQuantity}, gijų kiekis {_controller.ThreadQuantity}");

                //kuriam callbacka ProgressBar'ui
                var progress = new Progress<int>(percent =>
                {
                    prgrsBar.PerformStep();
                });


                //Leidziam gijas, gaunam rezultatą
                _stringGenerations = await ExecuteThreads(progress);

                //atnaujinam gridą
                RefreshGrid();
                WriteMessage($"Generavimas baigtas, sugeneruota ir išsaugota {_controller.GenerationCount} įrašų per {_controller.GenerationElapsed}ms");


            }
            catch (ArgumentException ae)
            {
                WriteMessage(ae.Message);
            }
            catch (Exception ex)
            {
                WriteMessage(ex.Message);
            }
            finally
            {
                //atstatom būsenas
                FinalizeGeneration();
            }


        }

        /// <summary>
        /// Po generavimo atsato mygtukus ir progress barą
        /// </summary>
        private void FinalizeGeneration()
        {
            //progress baras atstatomas
            prgrsBar.Value = 0;
            //atstatom mygtukus
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        /// <summary>
        /// Nustatom formą naujam generavimui
        /// </summary>
        private void ClearForm()
        {
            //nustatom progress bara
            prgrsBar.Value = 0;
            prgrsBar.Maximum = _controller.EntriesQuantity;
            prgrsBar.Step = 1;

            //disablinam mygtuką, kad neleistų dar kartą
            btnStart.Enabled = false;
            //enablinam mygtuką, kad galima būtų stabdyti
            btnStop.Enabled = true;

            //ištuštinam gridą
            InitGrid();
        }

        /// <summary>
        /// Gijos stabdymo mygtuko paspaudimas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStop_Click(object sender, EventArgs e)
        {
            CancelThreads();
        }

        /// <summary>
        /// Leidžiam generavimą ir gijas  _sqlMultithreadingModel objekte
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        private async Task<IList<StringGeneration>> ExecuteThreads(IProgress<int> progress)
        {
            var allEntries = await _controller.AssignAndRunTasksAsync(progress);
            return allEntries;
        }


        /// <summary>
        /// Stabdom gijas _sqlMultithreadingModel objekte
        /// </summary>
        private void CancelThreads()
        {
            if (!(_controller is null))
            {
                _controller.CancelThreads();
            }

        }

        /// <summary>
        /// Pranešimo atvaizdavimas status bare
        /// </summary>
        /// <param name="message"></param>
        private void WriteMessage(string message)
        {
            sttsStrp.Items["mainStatus"].Text = message;
        }

        /// <summary>
        /// Konvertuoja ir tikrina gijų kiekį
        /// </summary>
        /// <returns>Gijų kiekis</returns>
        private int GetThreadQuantity()
        {

            var threadQuantity = Convert.ToInt32(Math.Round(nmrcUpDwnThreadCounter.Value));
            //Pakeiciam i sveika skaiciu, jeigu įvesta su kableliu, kad matytusi kiek realiai yra uzleista thread'u
            nmrcUpDwnThreadCounter.Value = threadQuantity;
            if (threadQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(threadQuantity));
            }
            return threadQuantity;
        }


        /// <summary>
        /// Konvertuojam ir tikrinam įrašų kiekį
        /// </summary>
        /// <returns>Įrašų kiekis</returns>
        private int GetEntriesQuantity()
        {
            string entriesQuantity = txtBxEntriesQuantity?.Text;


            if (string.IsNullOrWhiteSpace(entriesQuantity))
            {
                throw new ArgumentOutOfRangeException(nameof(entriesQuantity));
            }
            if (!int.TryParse(entriesQuantity, out int intQuantity))
            {
                throw new ArgumentOutOfRangeException(nameof(entriesQuantity));
            }
            if (intQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(entriesQuantity));
            }
            return intQuantity;
        }



        private void InitGrid()
        {
            _stringGenerations = new List<StringGeneration>();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            mainGrid.DataSource = _stringGenerations;
            mainGrid.Update();
        }
    }
}
