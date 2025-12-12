using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation; // Нужно для навигации
using WpfApp1.Models; // Нужно для доступа к AppData (Core)

namespace WpfApp1.pages
{
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TxtName.Text = AppData.CurrentConfig.ClientName;
            TxtPhone.Text = AppData.CurrentConfig.ClientPhone;
            TxtEmail.Text = AppData.CurrentConfig.ClientEmail;

            ValidateInput();
        }

        private void Input_Changed(object sender, TextChangedEventArgs e)
        {
            ValidateInput();
        }

        private void ValidateInput()
        {
            bool isValid = !string.IsNullOrWhiteSpace(TxtName.Text) &&
                           !string.IsNullOrWhiteSpace(TxtPhone.Text);

            if (BtnNext != null)
            {
                BtnNext.IsEnabled = isValid;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            AppData.CurrentConfig.ClientName = TxtName.Text;
            AppData.CurrentConfig.ClientPhone = TxtPhone.Text;
            AppData.CurrentConfig.ClientEmail = TxtEmail.Text;

            NavigationService.Navigate(new Page5());
        }
    }
}