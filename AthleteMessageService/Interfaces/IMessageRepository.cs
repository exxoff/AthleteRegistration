
using AthleteMessageService.UserTypes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace AthleteMessageService.Interfaces
{
    [ServiceContract]
    public interface IMessageRepository
    {
        [OperationContract]
        void ReceiveMessage(string Message);
        
    }
}