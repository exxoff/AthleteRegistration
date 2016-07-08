using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteAdmin.Interfaces
{
    public interface IMessageText
    {
        ObservableCollection<ICustomText> MessageText { get; set; }
    }
}
