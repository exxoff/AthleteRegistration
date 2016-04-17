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
        public string DbFilename { get; set; }

        public List<AthleteDto> GetAllAthletes()
        {
            throw new NotImplementedException();
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
                    Console.WriteLine("Funktionär, {0} {1} registrerades.",Athlete.FirstName,Athlete.LastName);
                    return;
                }
                if (col.Count(x => x.Bib.Equals(Athlete.Bib)) > 0)
                {
                    col.Update(Athlete);
                    Console.WriteLine("{0} uppdaterades. ({1} {2}, {3} bana.)", Athlete.Bib,Athlete.FirstName,Athlete.LastName,Athlete.Course);


                }
                else
                {
                    col.Insert(Athlete);
                    Console.WriteLine("Ny deltagare, startummer {2}, {0} {1} lades till.", Athlete.FirstName, Athlete.LastName, Athlete.Bib);

                }
            }
        }
    }
}
