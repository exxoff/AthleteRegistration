using AthleteAdmin.UserTypes;
using System.Collections.Generic;
using System.ComponentModel;

namespace AthleteAdmin.Interfaces
{
    public interface IHostInfo
    {
        List<string> Addresses { get; set; }
        string DatabaseFile { get; set; }
        List<INetInfo> NetworkInfoList { get; set; }
        int Port { get; set; }

        
    }
}