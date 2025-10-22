using System.Windows;
using System.Windows.Controls;
using Hotel_3.WPF.ViewModels;

namespace Hotel_3.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    public partial class AuthView : UserControl
    {
        public AuthView()
        {
            InitializeComponent();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthViewModel vm)
            {
                vm.Password = PasswordBox.Password;
            }
        }
    }
}
