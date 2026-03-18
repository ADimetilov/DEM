using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DEM
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
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Current_user.fio = "Гость";
            Current_user.role = "Ghost";
            HeadWindow window = new HeadWindow();
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (DemContext db = new DemContext())
            {
                var users = db.Users.ToList();
                var roles = db.Roles.ToList();
                List<User> user = users.Where(p => p.Login == LoginBox.Text).ToList();
                if (user.Count > 0)
                {
                    if (user[0].Password == PasswordUserBox.Password)
                    {
                        Current_user.fio = user[0].Fio.ToString();
                        Current_user.role = user[0].Role.Role1.ToString();
                        HeadWindow window = new HeadWindow();
                        window.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }

                
        }
    }
}