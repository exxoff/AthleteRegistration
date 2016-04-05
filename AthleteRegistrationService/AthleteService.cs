using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace AthleteRegistrationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class AthleteService : IAthleteService
    {

        public static Queue<AthleteDto> AthleteQueue;
        public string GeAthlete(int Bib)
        {
            throw new NotImplementedException();
        }

        public AthleteDto GetNewAthlete()
        {
            return new AthleteDto();
        }

        public void Save(AthleteDto athlete)
        {
            DataHelper.SaveAthlete(athlete);
        }

        public void StartQueueTimer()
        {
           if(AthleteQueue == null)
            {
                AthleteQueue = new Queue<AthleteDto>();
            }

            System.Timers.Timer queueTimer = new System.Timers.Timer();
            queueTimer.Interval = 2000;
            queueTimer.Elapsed += QueueTimer_Elapsed;
            queueTimer.Enabled = true;
            queueTimer.Start();
        }

        private void QueueTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool wasMutexCreatedNew = false;
            using (Mutex onlyOne = new Mutex(true, "MutexWait", out wasMutexCreatedNew))
            {
                //Console.WriteLine("Checking queue...");
                if (wasMutexCreatedNew)
                {
                    try
                    {
                        while (AthleteQueue.Count > 0)
                        {
                            DataHelper.SaveAthlete(AthleteQueue.Dequeue());
                            
                        }
                    }
                    finally
                    {
                        onlyOne.ReleaseMutex();

                    }
                }
            }
        }

        public bool StoreAthlete(AthleteDto athlete)
        {
            try
            {
                if (AthleteQueue == null)
                {
                    AthleteQueue = new Queue<AthleteDto>();
                }
                //TODO: Check if BIB exist.
                AthleteQueue.Enqueue(athlete);
                return true;
            }
            catch (Exception ex)
            {
                DataHelper.LogError(string.Format("Kunde inte lägga till deltagare nummer {0}, {1} {2} i kön. Felmeddelande: {3}", athlete.Bib, athlete.FirstName, athlete.LastName, ex.Message), ex.InnerException.ToString());
                return false;
            }
            
        }
    }
}
