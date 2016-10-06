using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AthleteRegistrationService
{
    public static class DataHelper
    {
        //public static ObservableCollection<string> Messages;

        private static ObservableCollection<string> messages;

        public static ObservableCollection<string> Messages
        {
            get { return messages; }
            set { messages = value; }
        }


        //public static void SaveAthlete(AthleteDto athlete)
        //{
        //    using(var db = new LiteDatabase("C:\\Temp\\_athletes.LiteDB"))
        //    {
        //        var col = db.GetCollection<AthleteDto>("Athletes");
        //        if (col.Count(x => x.Bib.Equals(athlete.Bib)) > 0)
        //        {
        //            col.Update(athlete);
        //            Console.WriteLine("{0} uppdaterades", athlete.FirstName);


        //        }
        //        else
        //        {
        //            col.Insert(athlete);
        //            Console.WriteLine("Ny deltagare, startummer {2}, {0} {1} lades till.", athlete.FirstName,athlete.LastName,athlete.Bib);

        //        }
        //    }
        //}

        public static string ToTitleCase(this string text)
        {
            if(text != null)
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                text = textInfo.ToTitleCase(text);

            }


            return text;
        }

        //public static AthleteDto BibExists(int Bib)
        //{

            
        //    using (var db = new LiteDatabase("C:\\Temp\\_athletes.LiteDB"))
        //    {
        //        var col = db.GetCollection<AthleteDto>("Athletes");
        //        if (col.Count(x => x.Bib.Equals(Bib)) > 0)
        //        {

        //        }
        //    }

        //            return null;
        //}

        public static void LogError(string errorMessage,string innerException)
        {
            Console.WriteLine(errorMessage);
        }


    }
}
