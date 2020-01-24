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

namespace ICE3StarterCode
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

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            
            if (rdoDB.IsChecked == true && txtConvert.Text != "")
                //LineartoDB
                txtConvertOutput.Text = LineartoDB(int.Parse(txtConvert.Text))+"dB";

            else if (rdoLinear.IsChecked == true && txtConvert.Text != "")
                //DBtoLinear
                txtConvertOutput.Text = DBtoLinear(int.Parse(txtConvert.Text));

            else
                MessageBox.Show("There is missing data in the fields");
        }
        private String LineartoDB (int linearVal)
        {
            double convertLin= 10 * Math.Log10(linearVal);
            String convertStringlin = convertLin.ToString("N4");
            return convertStringlin;
        }
        private String DBtoLinear(double dbVal)
        {
            double newdbVal = dbVal / 10;
            double convertDB = Math.Pow(10.0,newdbVal);
            String convertStringDB = convertDB.ToString("N4");
            return convertStringDB;

        }

        private void btnComputeNoise_Click(object sender, RoutedEventArgs e)
        {
            if (cboTemperatureUnit.Text == "Kelvin")
            {//Check for Kelvin
                double tempKelvin = double.Parse(txtTemperature.Text);
                calcNoiseInDB(tempKelvin);
            }
            else if (cboTemperatureUnit.Text == "Celsius")
            {//Check for Celcius
                double celsius = CTOK(double.Parse(txtTemperature.Text));
                
                calcNoiseInDB(celsius);
            }
            else if (cboTemperatureUnit.Text == "Fahrenheit")
            {//check for fahrenheit
                double fahrenheit = FTOK(double.Parse(txtTemperature.Text));
                calcNoiseInDB(fahrenheit);
            }   

        }
        private double CTOK(double temp)
        {
            
            return temp+273.15;
        }
        
        private double FTOK(double temp)
        {
            double converted = (temp - 32) * (5 / 9) + (273.15);
            return converted;
        }

        private void calcNoiseInDB(double t)
        {
            
            double bandwidth = double.Parse(txtBandwidth.Text);
            double noise = (-228.6 + 10 * Math.Log10(t) + 10 * Math.Log10(bandwidth)+60);
            
            txtNoiseOutput.Text = noise.ToString();
        }
    }
}
