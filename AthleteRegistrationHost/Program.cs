using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistrationHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseUri = new Uri("http://localhost:9090/AthleteRegistration");

            using (ServiceHost host = new ServiceHost(typeof(AthleteRegistrationService.AthleteService),baseUri))
            {
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);
                host.Open();

                var hostProxy = new AthleteRegistrationService.AthleteService();

                hostProxy.StartQueueTimer();
                Console.WriteLine("Service listening on {0}",baseUri.ToString());
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();


            }
        }
    }
}
