using AthleteMessageService.Interfaces;
using AthleteMessageService.UserTypes;
using AthleteRegistrationService.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistrationService
{
    public class LiteDBClient : IDbClient
    {

        IMessageRepository repo = MessageRepository.Instance;
        public string DbFilename { get; set; }

        public List<AthleteDto> GetAllAthletes(bool IncludeCrew=false)
        {
            int selector = IncludeCrew ? -2 : 1;

            using (var db = new LiteDatabase(DbFilename))
            {
                var col = db.GetCollection<AthleteDto>("Athletes");
                var results = col.Find(Query.GTE("Bib", selector)).ToList();

                return results;
            }
        }

        public AthleteDto GetAthlete(int Bib)
        {
            using (var db = new LiteDatabase(DbFilename))
            {
                var col = db.GetCollection<AthleteDto>("Athletes");
                if (col.Count(x => x.Bib.Equals(Bib)) > 0)
                {
                    return col.FindOne(x => x.Bib.Equals(Bib));
                }

                return null;
            }
        }

        public void Save(AthleteDto Athlete)
        {
            using (var db = new LiteDatabase(DbFilename))
            {
                var col = db.GetCollection<AthleteDto>("Athletes");

                if(Athlete.Bib == -1)
                {
                    col.Insert(Athlete);
                    MessageClient.SendMessage(string.Format("Funktionär, {0} {1} registrerades.",Athlete.FirstName,Athlete.LastName));
                    return;
                }
                if (col.Count(x => x.Bib.Equals(Athlete.Bib)) > 0)
                {
                    col.Update(Athlete);
                    MessageClient.SendMessage(string.Format("{0} uppdaterades. ({1} {2}, {3} bana.)", Athlete.Bib,Athlete.FirstName,Athlete.LastName,Athlete.Group));


                }
                else
                {
                    col.Insert(Athlete);
                    //MessageClient client = new MessageClient();
                    MessageClient.SendMessage(string.Format("Ny deltagare, startummer {2}, {0} {1} lades till.", Athlete.FirstName, Athlete.LastName, Athlete.Bib));
                    //Console.WriteLine("Ny deltagare, startummer {2}, {0} {1} lades till.", Athlete.FirstName, Athlete.LastName, Athlete.Bib);

                }
            }
        }
    }
}
