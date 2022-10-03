using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Lab_3_CardGame
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IServiceGame" в коде и файле конфигурации.
    [ServiceContract(CallbackContract =typeof(IServiceGameCallBack))]
    public interface IServiceGame
    {

        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int id);
    }
    public interface IServiceGameCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallBack(string message);
    }
}
