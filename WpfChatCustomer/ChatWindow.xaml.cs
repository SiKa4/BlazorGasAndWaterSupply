using BlazorContolWork.Data;
using BlazorGasAndWaterSupply.Data;
using Microsoft.AspNetCore.SignalR.Client;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfChatCustomer
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        [BsonId]
        ObjectId _projectId;
        User thisUser;
        [BsonId]
        ObjectId idReceiver;
        List<ClassProjectNameDevDes> DevDesProject;
        HubConnection connection;

        public ChatWindow(User user)
        {
            InitializeComponent();
            this.thisUser = user;
            DevDesProject = new List<ClassProjectNameDevDes>();
            FillList();
            ListChatDevDes.ItemsSource = DevDesProject;
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5012/ChatHub")
                .Build();
        }

        private void FillList()
        {
            foreach(var i in MongoExamples.SearchProjectCustomer(thisUser._id))
            {
                var tempDes = MongoExamples.FindId(i._idDesigner);
                DevDesProject.Add(new ClassProjectNameDevDes(tempDes.Name, i.TypeProject, i.Name, i._idDesigner, i._id));
                var tempDev = MongoExamples.FindId(i._idDeveloper);
                DevDesProject.Add(new ClassProjectNameDevDes(tempDev.Name, i.TypeProject, i.Name, i._idDeveloper, i._id));
            }
        }

        private void FilledMessagesInChat()
        {
            var temp = MongoExamples.SearchMessagesThisChat(thisUser._id, idReceiver, _projectId);
            if (temp == null) return;
            foreach (var i in temp)
            {
                messagesList.Items.Add($"{i.NameSending}: {i.Message}");
            }
        }


        private async void ListChatDevDes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            messagesList.Items.Clear();
            ClassProjectNameDevDes temp = (ClassProjectNameDevDes)ListChatDevDes.SelectedItem;
            _projectId = temp._idProject;
            idReceiver = temp._id;
            FilledMessagesInChat();
            await connection.DisposeAsync();
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5012/ChatHub")
                .Build();
            if (temp != null)
            {
                connection.On<string, string, string, string, string>("ReceiveMessage", (user, message, _idReceiver, _idSending, _idProject) =>
                {
                    if((_idSending == thisUser._id.ToString() && _idReceiver == temp._id.ToString() 
                    || _idReceiver == thisUser._id.ToString() && _idSending == temp._id.ToString()) 
                    && _idProject == _projectId.ToString())
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            var newMessage = $"{user}: {message}";
                            messagesList.Items.Add(newMessage);
                        });
                    }
                });

                try
                {
                    await connection.StartAsync();
                    messagesList.Items.Add("Connection started");
                    BtnSend.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    messagesList.Items.Add(ex.Message);
                }
            }
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendMessage",
                    thisUser.Name, txtMessage.Text, idReceiver.ToString(), thisUser._id.ToString(), _projectId.ToString());
                txtMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }
    }
}
