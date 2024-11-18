using System.Windows;

namespace WpfApp2
{
    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            InitializeComponent();
        }

        private void CalculateNetto_Click(object sender, RoutedEventArgs e)
        {

            if (decimal.TryParse(BruttoInput.Text, out decimal brutto))
            {

                decimal stawka = Oblicz_Stawke();


                decimal netto = brutto * (1 - stawka);


                BruttoList.Items.Add($"Brutto: {brutto}zł");
                NettoList.Items.Add($"Netto: {netto}zł");
            }
            else
            {
                MessageBox.Show("Wprowadź poprawną wartość dla pensji brutto.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private decimal Oblicz_Stawke()
        {
            if (stawk8_5.IsChecked == true)
                return 0.085m;
            if (stawk19.IsChecked == true)
                return 0.19m;
            return 0.12m;
        }
    }
}