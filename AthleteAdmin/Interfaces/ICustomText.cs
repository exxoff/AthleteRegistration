using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AthleteAdmin.Interfaces
{
    public interface ICustomText
    {
        string Text { get; set; }
        Color TextColor { get; set; }

        
    }
}
