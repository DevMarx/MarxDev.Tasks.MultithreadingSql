
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarxDev.Tasks.MultithreadingSql
{
    public partial class MainForm : Form
    {
        private SqlMultithreadingController _sqlMultithreadingModel;

        public MainForm()
        {
            InitializeComponent();
           
            
        }

        private void TxtBxEntriesQuantityKeyPress(object sender, KeyPressEventArgs e)
        {
            //Patikrinam, kad butu sveikas skaicius
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void BtnStartClick(object sender, EventArgs e)
        {
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["StringGenerationsConnectionString"].ConnectionString;
                _sqlMultithreadingModel = new SqlMultithreadingController(connectionString);

                _sqlMultithreadingModel.Init( GetThreadQuantity(), GetEntriesQuantity());             


                prgrsBar.Value = 0;
                prgrsBar.Maximum = _sqlMultithreadingModel.EntriesQuantity;
                prgrsBar.Step = 1;

                //disablinam mygtuką, kad neleistų dar kartą
                btnStart.Enabled = false;
                //enablinam mygtuką, kad galima būtų stbdyti
                btnStop.Enabled = true;

                //kuriam callbacka ProgressBar'ui
                var progress = new Progress<int>(percent =>
                {
                    prgrsBar.PerformStep();
                });

                //Leidziam gijas
                await ExecuteThreads(progress);

                    
            }
            catch (ArgumentException ae)
            {
                sttsStrp.Items["mainStatus"].Text = ae.Message;
            }
            catch (Exception ex)
            {
                sttsStrp.Items["mainStatus"].Text = ex.Message;
            }
            finally
            {
                //atstatom mygtukus
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }


        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            CancelThreads();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        private async Task<IList<StringGeneration>> ExecuteThreads(IProgress<int> progress)
        {          
            var allentries=await _sqlMultithreadingModel.AssignAndRunTasksAsync(progress);
            return allentries;
        }


        /// <summary>
        /// Stabdom gijas _sqlMultithreadingModel objekte
        /// </summary>
        private void CancelThreads()
        {
            if (!(_sqlMultithreadingModel is null))
            {
                _sqlMultithreadingModel.CancelThreads();
            }

        }

        /// <summary>
        /// Konvertuoja ir tikrina gijų kiekį
        /// </summary>
        /// <returns>Gijų kiekis</returns>
        private int GetThreadQuantity()
        {

            var threadQuantity = Convert.ToInt32(Math.Round(nmrcUpDwnThreadCounter.Value));
            //Pakeiciam i sveika skaiciu, kad matytusi kiek realiai yra uzleista thread'u
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


    }
}
