using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int TotalSteps = 5;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new pages.Page1());
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            int currentStep = 1;

            if (e.Content is pages.Page1) currentStep = 1;
            else if (e.Content is pages.Page2) currentStep = 2;
            else if (e.Content is pages.Page3) currentStep = 3;
            else if (e.Content is pages.Page4) currentStep = 4;
            else if (e.Content is pages.Page5_Summary) currentStep = 5;

            double progressValue = (double)currentStep / TotalSteps * 100;

            AppProgressBar.Value = progressValue;
        }
    }
}
    