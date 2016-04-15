using AthleteRegistrationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistrationService.Factory
{
    public static class DbFactory
    {
        public static IDbClient GetDbClient(string ClientType)
        {
            return new LiteDBClient();
        }
    }
}
