using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistrationService.Interfaces
{
    public interface IDbClient
    {
        void Save(AthleteDto Athlete);

        AthleteDto GetAthlete(int Bib);

        List<AthleteDto> GetAllAthletes();
        
    }
}
