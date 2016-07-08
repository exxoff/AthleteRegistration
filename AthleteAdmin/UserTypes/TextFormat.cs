using AthleteAdmin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AthleteAdmin.UserTypes
{
    class TextFormat : ICustomText
    {
        public string Text { get; set; }

        public Color TextColor { get; set; }


        
    }
}
