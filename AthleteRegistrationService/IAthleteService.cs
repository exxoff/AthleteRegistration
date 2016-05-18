using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AthleteRegistrationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAthleteService
    {
        [OperationContract]
        void SetDatabaseType(string Type);

        [OperationContract]
        void SetDatabaseFile(string File);

        [OperationContract]
        void StartQueueTimer();

        [OperationContract]
        bool StoreAthlete(AthleteDto athlete);

        [OperationContract]
        List<AthleteDto> GetAllAthletes(bool IncludeCrew=false);

        [OperationContract]
        AthleteDto ExistingAthlete(int Bib);

        [OperationContract]
        AthleteDto GetNewAthlete();

        [OperationContract]
        bool IsAlive();
    }


}
