using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthleteRegistration.UserTypes;
using System.ServiceModel;
using AthleteRegistrationService;

namespace AthleteRegistration.Utils
{
    public static class ServiceHelper
    {

        public static string ServerAddress;
        public static ChannelFactory<IAthleteService> AthleteChannelFactory = null;

        public static List<Athlete> ToAthleteCollection(this List<AthleteDto> ListOfremoteAthletes)
        {
            List<Athlete> Athletes = new List<Athlete>();
            foreach (AthleteDto dto in ListOfremoteAthletes)
            {
                Athlete _currentAthlete = new Athlete();
                _currentAthlete.Bib = dto.Bib;
                _currentAthlete.FirstName = dto.FirstName;
                _currentAthlete.LastName = dto.LastName;
                _currentAthlete.Group = dto.Group;
                _currentAthlete.WaveNumber = dto.WaveNumber;
                _currentAthlete.EMail = dto.EMailAddress;

                Athletes.Add(_currentAthlete);
            }
            return Athletes;
        }

        public static ChannelFactory<IAthleteService> GetFactory()
        {

            if (AthleteChannelFactory == null)
            {
                var serviceAddress = ServiceHelper.ServerAddress;
                var binding = new NetTcpBinding();

                AthleteChannelFactory = new ChannelFactory<IAthleteService>(binding, serviceAddress);
            }


                return AthleteChannelFactory;

        }
    }
}
