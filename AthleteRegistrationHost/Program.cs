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

            Uri baseUri = new Uri(args[0]);

            try
            {
                

                using (ServiceHost host = new ServiceHost(typeof(AthleteRegistrationService.AthleteService), baseUri))
                {
                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                    
                    host.Description.Behaviors.Add(smb);
                    host.Open();

                    var hostProxy = new AthleteRegistrationService.AthleteService();
                    hostProxy.SetDatabaseType(args[1]);
                    hostProxy.SetDatabaseFile(args[2]);
                    hostProxy.StartQueueTimer();
                    Console.WriteLine("Värden lyssnar på adress {0}",baseUri.ToString());
                    Console.WriteLine("Tryck <Enter> för att stoppa servicen.");
                    Console.ReadLine();
                }

            }

            catch (System.ServiceModel.CommunicationObjectFaultedException)
            {
                Console.WriteLine("Kunde inte starta värden på {0}. Kontrollera så den är startad med Administratörsrättigheter (se manual).",baseUri.ToString());
                Console.WriteLine("Tryck <Enter> för att stoppa servicen.");
                Console.ReadLine();
            }
            catch (System.ServiceModel.AddressAlreadyInUseException)
            {
                Console.WriteLine("Adressen {0} används redan",baseUri.ToString());
                Console.WriteLine("Tryck <Enter> för att stoppa servicen.");
                Console.ReadLine();
            }
            catch (System.ServiceModel.AddressAccessDeniedException)
            {
                Console.WriteLine("Åtkomst till adressen {0} nekades. OBS! AthleteRegistrationHost måste startas med Administratörsrättigheter.",baseUri.ToString());
                Console.WriteLine("Tryck <Enter> för att stoppa servicen.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Tryck <Enter> för att stoppa servicen.");
                Console.ReadLine();
            }
            finally
            {
 
            }


            
        }
    }
}
