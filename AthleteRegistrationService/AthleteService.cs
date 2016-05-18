using AthleteRegistrationService.Factory;
using AthleteRegistrationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace AthleteRegistrationService
{
    
    public class AthleteService : IAthleteService
    {

        private static Queue<AthleteDto> AthleteQueue;
        private static IDbClient client;
        private static string DatabaseType;
        private static string DbFilename;

        public AthleteDto GetNewAthlete()
        {
            return new AthleteDto();
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
                GetDbClient();
                
                
                if (wasMutexCreatedNew)
                {
                    try
                    {
                        while (AthleteQueue.Count > 0)
                        {
                            
                            client.Save(AthleteQueue.Dequeue());
                            
                            
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

                AthleteQueue.Enqueue(athlete);
                return true;
            }
            catch (Exception ex)
            {
                DataHelper.LogError(string.Format("Kunde inte lägga till deltagare nummer {0}, {1} {2} i kön. Felmeddelande: {3}", athlete.Bib, athlete.FirstName, athlete.LastName, ex.Message), ex.InnerException.ToString());
                return false;
            }
            
        }

        public List<AthleteDto> GetAllAthletes(bool IncludeCrew = false)
        {
            client = GetDbClient();
            return client.GetAllAthletes(IncludeCrew);
        }

        public AthleteDto ExistingAthlete(int Bib)
        {

            if(Bib == -1)
            {
                return null;
            }

           client =GetDbClient();
            
            return client.GetAthlete(Bib);

            
        }

        private IDbClient GetDbClient()
        {
            if (client == null)
            {
                client = DbFactory.GetDbClient(DatabaseType);
                client.DbFilename = DbFilename;
            }

            return client;

        }

        public void SetDatabaseType(string Type)
        {
            DatabaseType = Type;
        }

        public void SetDatabaseFile(string File)
        {
            DbFilename = File;
        }

        public bool IsAlive()
        {
            return true;
        }
    }
}
