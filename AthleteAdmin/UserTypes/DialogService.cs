using AthleteAdmin.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteAdmin.UserTypes
{
    public class DialogService : IDialogService
    {
        public string FileOpenDialog(string DefaultPath)
        {
            var f = new Microsoft.Win32.OpenFileDialog();
            f.FileName = new FileInfo(DefaultPath).Name;
            f.CheckFileExists = false;
            f.Filter = "*.aReg|*.aReg";
            f.InitialDirectory = Path.GetTempPath();
            if ((bool)f.ShowDialog())
            {
                return f.FileName;
            }
            return null;

        }
    }
}
