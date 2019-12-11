using DollarWpfClient.DollarServiceReference;
using System;
using System.Windows;

namespace DollarWpfClient
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DollarClient dc;   
        public MainWindow()
        {
            InitializeComponent();
            dc = new DollarClient();
            dc.Open();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Currency.Content = dc.Convert(InputCurrency.Text);
            }
            catch(TimeoutException)
            {
                MessageBox.Show("Timeout");
            }
            catch (System.ServiceModel.FaultException<ArgumentNullException>)
            {
                MessageBox.Show("Please enter a number");
            }
            catch (System.ServiceModel.FaultException<FormatException>)
            {
                MessageBox.Show("Sadly it isn't proper number");
            }
            catch (System.ServiceModel.FaultException<ArgumentOutOfRangeException>)
            {
                MessageBox.Show("Number is too big");
            }
            catch (System.ServiceModel.CommunicationException c)
            {
                MessageBox.Show($"There was a communication problem. Probably restart is needed {c.Message}");
            }
        }
    }
}
