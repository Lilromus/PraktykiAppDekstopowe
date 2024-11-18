using System.Windows;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text;
            string password = PasswordInput.Password;

           
            if (username == "Admin" && password == "Owca")
            {
                
                CalculatorWindow calculatorWindow = new CalculatorWindow();
                calculatorWindow.Show();
                this.Close();
            }
            else
            {
                
                ErrorText.Text = "Niepoprawna nazwa użytkownika lub hasło.";
            }
        }
    }
}