using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteAdmin.Interfaces
{
    public interface IDialogService
    {
        string FileOpenDialog();
        bool DisplayYesNoMessageBoxDialog(string Message, string Title, bool showCancelButton);
    }
}
