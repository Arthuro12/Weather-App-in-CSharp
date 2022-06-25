using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public string cityName = "Paris";

        public string resultJsonString;
        //public string uri = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", cityName, apiKey);

        public MainWindow()
        {
            InitializeComponent();

            SetHeaderImage();
            SetDay(); //Date
            GetApiResponse();
            SetUiInfo();
         
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            cityName = cityNameText.Text;
            cityTitle.Content = cityName;
        }

        /// <summary>
        /// Display the inos of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hergestellt von Arthur Foadjo", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Get the response of our api call
        /// </summary>
        public void GetApiResponse()
        {
            string apiKey = "1a0ee8bf4a164f9a0a73f812da9853f2";
            string baseApiString = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", cityName, apiKey);

            using (WebClient wc = new WebClient())
            {
                // Loading of the JSON from the API
                resultJsonString = wc.DownloadString(baseApiString);
            }

        }

        /// <summary>
        /// Parse the data to inject them in the user interface
        /// </summary>
        public void SetUiInfo()
        {
            JObject jo = JObject.Parse(resultJsonString);
            MessageBox.Show(resultJsonString);
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

        /// <summary>
        /// Load the image of the Header
        /// </summary>
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
