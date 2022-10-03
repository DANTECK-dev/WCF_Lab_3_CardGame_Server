using System.ServiceModel;

namespace WCF_Lab_3_CardGame
{
    internal class ServerUser
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
