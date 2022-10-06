using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Lab_3_CardGame
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceGame : IServiceGame
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextID = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextID++,
                Name = name,
                operationContext = OperationContext.Current
            };
            users.Add(user);
            SendMessage(user.Name + " connected", 0);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.ID == id);
            if(user != null)
            {
                users.Remove(user);
                SendMessage(user.Name + " disconnected", 0);
            }
        }
        
        public void SendMessage(string message, int id)
        {
            foreach(var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(x => x.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name + " ";
                }
                answer += message;
                item.operationContext.GetCallbackChannel<IServiceGameCallBack>().MessageCallBack(answer);  

            }
        }

    }
}
