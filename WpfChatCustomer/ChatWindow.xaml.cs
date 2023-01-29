using BlazorContolWork.Data;
using Microsoft.AspNetCore.SignalR.Client;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
using System.Windows.Shapes;

namespace WpfChatCustomer
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        User userr;
        [BsonId]
        ObjectId idReceiver;
        List<ClassProjectNameDevDes> DevDesProject;
        HubConnection connection;
        public ChatWindow(User user)
        {
            InitializeComponent();
            this.userr = user;
            DevDesProject = new List<ClassProjectNameDevDes>();
            FillList();
            ListChatDevDes.ItemsSource = DevDesProject;


            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5012/ChatHub")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        private void FillList()
        {
            foreach(var i in MongoExamples.SearchProjectCustomer(userr._id))
            {
                var tempDes = MongoExamples.FindId(i._idDesigner);
                DevDesProject.Add(new ClassProjectNameDevDes(tempDes.Name, tempDes.Department, i.TypeProject, i._idDesigner));
                var tempDev = MongoExamples.FindId(i._idDeveloper);
                DevDesProject.Add(new ClassProjectNameDevDes(tempDev.Name, tempDev.Department, i.TypeProject, i._idDeveloper));
            }
        }

        private async void ListChatDevDes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            messagesList.Items.Clear();
            ClassProjectNameDevDes temp = (ClassProjectNameDevDes)ListChatDevDes.SelectedItem;
            idReceiver = temp._id;
            if (temp != null)
            {
                connection.On<string, string, string, string>("ReceiveMessage", (user, message, _idReceiver, _idSending) =>
                {
                    if(_idSending == userr._id.ToString() && _idReceiver == temp._id.ToString() || _idReceiver == userr._id.ToString() && _idSending == temp._id.ToString())
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
                    userr.Name, txtMessage.Text, idReceiver.ToString(), userr._id.ToString());
                txtMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }
    }
}
