using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistrationService
{
    [DataContract]
    public class AthleteDto
    {
        [DataMember]
        [BsonId]
        public int AthleteId { get; set; }
        [DataMember]
        //[BsonId]
        public int Bib { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Group { get; set; }
        [DataMember]
        public string WaveNumber { get; set; }
        [DataMember]
        public string EMailAddress { get; set; }
      


        public DateTime Updated { get; set; }
    }
}
