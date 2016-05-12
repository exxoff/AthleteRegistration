using AthleteRegistration.UserTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistration.Utils
{
    public static class CsvHelper
    {

        public static void GenerateCsv(FileInfo OutputFile, List<Athlete> Athletes)
        {
            using (StreamWriter writer = new StreamWriter(OutputFile.FullName))
            {

          
                writer.WriteLine("BIB,Wave Number,First Name, Last Name,Group,EMail");

                foreach(var _athlete in Athletes)
                {
                    string line = string.Format("{0},{1},{2},{3},{4},{5}", _athlete.Bib, _athlete.WaveNumber, _athlete.FirstName, _athlete.LastName, _athlete.Group, _athlete.EMail);
                    writer.WriteLine(line);
                }
            }
        }
    }
}
