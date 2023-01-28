using BlazorContolWork.Data;
using Microsoft.AspNetCore.SignalR.Client;
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
        User user;
        List<ClassProjectNameDevDes> DevDesProject;
        HubConnection connection;
        public ChatWindow(User user)
        {
            InitializeComponent();
            this.user = user;
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
            foreach(var i in MongoExamples.SearchProjectCustomer(user._id))
            {
                var tempDes = MongoExamples.FindId(i._idDesigner);
                DevDesProject.Add(new ClassProjectNameDevDes(tempDes.Name, tempDes.Department, i.TypeProject, i._idDesigner));
                var tempDev = MongoExamples.FindId(i._idDeveloper);
                DevDesProject.Add(new ClassProjectNameDevDes(tempDev.Name, tempDev.Department, i.TypeProject, i._idDeveloper));
            }
        }

        private async void ListChatDevDes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClassProjectNameDevDes temp = (ClassProjectNameDevDes)ListChatDevDes.SelectedItem;
            if(temp != null)
            {
                connection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        var newMessage = $"{user}: {message}";
                        messagesList.Items.Add(newMessage);
                    });
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
                    user.Name, txtMessage.Text);
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }
    }
}
