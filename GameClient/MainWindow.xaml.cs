using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameClient.ServiceGame;

namespace GameClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceGameCallback
    {
        bool isConnected = false;
        ServiceGameClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            try
            {
                if (!isConnected)
                {
                    client = new ServiceGameClient(new System.ServiceModel.InstanceContext(this));
                    ID = client.Connect(TB_UserName.Text);
                    TB_UserName.IsEnabled = false;
                    BT_Connect.Content = "Disconnect";
                    isConnected = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if(client != null)
                    client = null;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                TB_UserName.IsEnabled = true;
                BT_Connect.Content = "Connect";
                isConnected = false;
            }
        }

        private void BT_Connect_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MessageCallBack(string message)
        {
            Chat.Items.Add(message);
            Chat.ScrollIntoView(Chat.Items[Chat.Items.Count - 1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void Message_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMessage(Message.Text, ID);
                    Message.Text = String.Empty;
                }
            }
        }
    }
}
