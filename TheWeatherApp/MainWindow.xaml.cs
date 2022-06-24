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

namespace TheWeatherApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WeatherData weather = new WeatherData();

        public MainWindow()
        {
            InitializeComponent();

            SetHeaderImage();
            SetDay(); //Date
         
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void buttonInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Set the Date of the current day
        /// </summary>
        public void SetDay()
        {
            //To-Do: Implement test for the function
            DateTime dateTime = DateTime.Now;
            weather.Date = dateTime;
            dateText.Content = weather.Date.ToString("MM/dd/yyyy");

            // Date of the first day
            weather.Forecast = weather.Date.ToString("ddd");
            forecast1.Content = weather.Forecast;

            // Date of the second day
            weather.Forecast = weather.Date.AddDays(1).ToString("ddd");
            forecast2.Content = weather.Forecast;

            // Date of the third day
            weather.Forecast = weather.Date.AddDays(2).ToString("ddd");
            forecast3.Content = weather.Forecast;
        }

        public void SetHeaderImage()
        {
            string meteo = "pluie";

            // Changes the image by getting the 
            // Create source
            BitmapImage bi = new BitmapImage();
            //Set the image source
            headerImage.Source = bi;
            //headerImage.Source = new BitmapImage(new Uri(@"/rain.jpg", UriKind.Relative));

            if (meteo == "pluie")
            {
                bi.BeginInit();
                bi.UriSource = new Uri(@"/rain.jpg", UriKind.Relative);
                bi.EndInit();
            }
            else
            {

            }
        }
    }
}
