using BlazorContolWork.Data;
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

namespace WpfChatCustomer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text != string.Empty && txtPassword.Password != string.Empty)
            {
                var check = MongoExamples.FindLogPassCustomer(txtLogin.Text, txtPassword.Password);
                if (check != null)
                {
                    ChatWindow win = new ChatWindow(check);
                    win.Show();
                    App.Current.MainWindow.Close();
                }
                else MessageBox.Show("You entered incorrect data!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("You have not entered the data!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
