using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        private List<decimal> bruttoList = new List<decimal>();
        private List<decimal> nettoList = new List<decimal>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateNetto_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                decimal brutto = decimal.Parse(BruttoInput.Text);

                decimal netto = brutto * 0.88m;


                bruttoList.Add(brutto);
                nettoList.Add(netto);


                BruttoList.Items.Add($"Brutto: {brutto}zł");
                NettoList.Items.Add($"Netto: {netto}zł");


                BruttoInput.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadź poprawną liczbę dla pensji brutto.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}