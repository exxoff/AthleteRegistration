using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace AthleteRegistrationHost
{
    class Program
    {

        const ConsoleColor CONSOLE_ERROR = ConsoleColor.Red;
        const ConsoleColor CONSOLE_BORDER = ConsoleColor.Cyan;
        const ConsoleColor CONSOLE_INFO = ConsoleColor.Yellow;
        const ConsoleColor CONSOLE_OK = ConsoleColor.Green;

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
                    WriteToConsole(new string('=',74), CONSOLE_BORDER);
                    WriteToConsole(string.Format("AthleteRegistration server v {0}.",GetVersion()),CONSOLE_INFO);
                    WriteToConsole(" ", ConsoleColor.Black);
                    WriteToConsole(string.Format("Värden lyssnar på adress {0}",baseUri.ToString()),CONSOLE_INFO);
                    WriteToConsole(" ", ConsoleColor.Black);
                    WriteToConsole(string.Format("Databas: {0}", args[2]), CONSOLE_INFO);
                    WriteToConsole(" ", ConsoleColor.Black);

                    WriteToConsole("Tryck <Enter> för att stoppa servicen.",CONSOLE_OK);
                    WriteToConsole(new string('=', 74),  CONSOLE_BORDER);
                    Console.ReadLine();
                }

            }

            catch (System.ServiceModel.CommunicationObjectFaultedException)
            {
                WriteToConsole(string.Format("Kunde inte starta värden på {0}. Kontrollera så den är startad med Administratörsrättigheter (se manual).",baseUri.ToString()), CONSOLE_ERROR);
                WriteToConsole("Tryck <Enter> för att stoppa servicen.", CONSOLE_ERROR);
                Console.ReadLine();
            }
            catch (System.ServiceModel.AddressAlreadyInUseException)
            {
                WriteToConsole(string.Format("Adressen {0} används redan",baseUri.ToString()),CONSOLE_ERROR);
                WriteToConsole("Tryck <Enter> för att stoppa servicen.", CONSOLE_ERROR);
                Console.ReadLine();
            }
            catch (System.ServiceModel.AddressAccessDeniedException)
            {
                WriteToConsole(string.Format("Åtkomst till adressen {0} nekades. OBS! AthleteRegistrationHost måste startas med Administratörsrättigheter.",baseUri.ToString()), CONSOLE_ERROR);
                WriteToConsole("Tryck <Enter> för att stoppa servicen.", CONSOLE_ERROR);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                WriteToConsole(ex.Message, CONSOLE_ERROR);
                WriteToConsole("Tryck <Enter> för att stoppa servicen.", CONSOLE_ERROR);
                Console.ReadLine();
            }
            finally
            {
 
            }


            
        }

        private static string GetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return string.Format("{0}.{1}.{2}", version.Major, version.MajorRevision, version.Minor);

              
        }

        private static void WriteToConsole(string text,ConsoleColor color)
        {

            Console.ForegroundColor = color;
            Console.WriteLine("{0}", text.PadRight(74));
            Console.ResetColor();

                
        }
    }
}
