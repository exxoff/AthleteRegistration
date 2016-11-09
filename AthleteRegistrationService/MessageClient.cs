using AthleteMessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace AthleteRegistrationService
{
    public static class MessageClient
    {
               


        public static void SendMessage(string Message)
        {

            string address = "net.pipe://localhost/AthleteRegMessage";
            Binding binding = new NetNamedPipeBinding();

            var factory = new ChannelFactory<IMessageRepository>(binding, address);

            var client = factory.CreateChannel();

            client.ReceiveMessage(Message);
        }
    }
}
