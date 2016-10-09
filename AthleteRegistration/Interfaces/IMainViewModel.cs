using AthleteRegistration.UserTypes;
using System.Collections.Generic;

namespace AthleteRegistration.Interfaces
{
    public interface IMainViewModel
    {
        string this[string columnName] { get; }

        int AthleteId { get; set; }
        int Bib { get; set; }
        Course CurrentCourse { get; set; }
        string EMailAddress { get; set; }
        string Error { get; }
        string FirstName { get; set; }
        bool HideEmailField { get; set; }
        bool IsAlive { get; set; }
        bool? IsCrew { get; set; }
        bool IsNew { get; set; }
        bool IsSaved { get; set; }
        string LastName { get; set; }
        string SaveMessage { get; set; }
        List<Course> Courses { get; set; }

        void ResetViewModel();
    }
}