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
        public string apiKeyWeather = "DPENNZP9VFN5Z9F94H4HBCLQ5";
        public string apiKeyNews = "pub_8812260346ba1aa1ad7b2cd5d6f1431276f2";
        public string cityName = "Berlin";
        public JObject jo1;
        public string resultJsonString;
        public string resultNewsJsonString;
   
        public MainWindow()
        {
            InitializeComponent();

            SetHeaderImage();
            SetDay(); // Date
            GetApiWeatherResponse();
            //LoadNews();
         
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            cityName = cityNameText.Text;
            cityTitle.Content = cityName;

            GetApiWeatherResponse();
            LoadNews();
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
        public void GetApiWeatherResponse()
        {
            string baseApiString = String.Format("https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/p={0}?lang=de&unitGroup=metric&key={1}", cityName, apiKeyWeather);
            try
            {
                using (WebClient wc = new WebClient())
                {
                    // Loading of the JSON from the API
                    resultJsonString = wc.DownloadString(baseApiString);

                    // Encoding to handle special characters
                    byte[] bytes = Encoding.Default.GetBytes(resultJsonString);
                    resultJsonString = Encoding.UTF8.GetString(bytes);
                    SetUiWeatherInfo();
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadNews()
        {
            //string apiNewsString = "https://newsapi.org/v2/everything?q=Berlin&language=de&pageSize=6&apiKey=162ea55002ee47f0acee3ce862cffa73";
            string apiNewsString = String.Format("https://newsdata.io/api/1/news?apikey={0}&language=de,fr,en&q={1}&page=6&country=de,fr,ca,us", apiKeyNews, cityName);
            try
            {
                using (WebClient wc = new WebClient())
                {
                    // Loading of the JSON from the API
                    resultNewsJsonString = wc.DownloadString(apiNewsString);

                    // Encoding to handle special characters
                    byte[] bytes = Encoding.Default.GetBytes(resultNewsJsonString);
                    resultNewsJsonString = Encoding.UTF8.GetString(bytes);
                    SetUiNewsInfo();
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MessageBox.Show(resultNewsJsonString);
        }

        /// <summary>
        /// Parse the data to inject them in the user interface
        /// </summary>
        public void SetUiWeatherInfo()
        {
            jo1 = JObject.Parse(resultJsonString); 

            weather.Description = Convert.ToString(jo1["days"][0]["description"]); 
            weatherDescription.Content = weather.Description;
            weather.Temperatur = Convert.ToString(jo1["days"][0]["temp"]);
            temperaturLabel1.Content = weather.Temperatur.Substring(0, 2) + "°C";
            weather.Wind = Convert.ToString(jo1["days"][0]["windspeed"]);
            windContent.Content = weather.Wind + "m/s";
            weather.TemperaturDayOne = Convert.ToString(jo1["days"][0]["temp"]);
            temperaturLabel2.Content = weather.TemperaturDayOne.Substring(0, 2) + "°C";
            weather.TemperaturDayTwo = Convert.ToString(jo1["days"][1]["temp"]);
            temperaturLabel3.Content = weather.TemperaturDayTwo.Substring(0, 2) + "°C";
            weather.TemperaturDayThree = Convert.ToString(jo1["days"][2]["temp"]);
            temperaturLabel4.Content = weather.TemperaturDayThree.Substring(0, 2) + "°C";
            SetWeatherIcon();
            
        }

        public void SetUiNewsInfo()
        {
            JObject jo2 = JObject.Parse(resultNewsJsonString);
            List<Article> articles = new List<Article>();
            for (int i = 0; i < jo2["results"].Count() - 1; i++)
            {
                string articleTitle = Convert.ToString(jo2["results"][i]["title"]);
                articles.Add(new Article() { Title = articleTitle });
            }
            
            if (articles.Count != 0)
            {
                articlesList.ItemsSource = articles;
            }
            else
            {
                articles.Add(new Article() { Title = "Es gibt zurzeit leider keinen Artikel für diese Stadt !!" });
                articles.Add(new Article() { Title = "Es gibt zurzeit leider keinen Artikel für diese Stadt !!" });
                articles.Add(new Article() { Title = "Es gibt zurzeit leider keinen Artikel für diese Stadt !!" });
                articles.Add(new Article() { Title = "Es gibt zurzeit leider keinen Artikel für diese Stadt !!" });
                articles.Add(new Article() { Title = "Es gibt zurzeit leider keinen Artikel für diese Stadt !!" });
                articlesList.ItemsSource = articles;
            }
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
      
            // Changes the image by getting the 
            // Create source
            BitmapImage bi1 = new BitmapImage();
            // Set the image source
            bi1.BeginInit();
            bi1.UriSource = new Uri(@"/rain.jpg", UriKind.Relative);
            bi1.EndInit();

            headerImage.Source = bi1;
        }

        public void SetWeatherIcon()
        {
            BitmapImage bi2 = new BitmapImage();
            BitmapImage bi3 = new BitmapImage();
            BitmapImage bi4 = new BitmapImage();
            string iconNameDayOne = Convert.ToString(jo1["days"][0]["icon"]);
            string iconNameDayTwo = Convert.ToString(jo1["days"][1]["icon"]);
            string iconNameDayThree = Convert.ToString(jo1["days"][2]["icon"]);

            switch (iconNameDayOne)
            {
                case "cloudy":
                    bi2.BeginInit();
                    bi2.UriSource = new Uri(@"Icons/cloudy.png", UriKind.Relative);
                    bi2.EndInit();
                    iconDayOne.Source = bi2;
                    break;
                case "rain":
                    bi2.BeginInit();
                    bi2.UriSource = new Uri(@"Icons/rain.png", UriKind.Relative);
                    bi2.EndInit();
                    iconDayOne.Source = bi2;
                    break;
                case "snow":
                    bi2.BeginInit();
                    bi2.UriSource = new Uri(@"Icons/snow.png", UriKind.Relative);
                    bi2.EndInit();
                    iconDayOne.Source = bi2;
                    break;
                case "wind":
                    bi2.BeginInit();
                    bi2.UriSource = new Uri(@"Icons/wind.png", UriKind.Relative);
                    bi2.EndInit();
                    iconDayOne.Source = bi2;
                    break;
                default:
                    bi2.BeginInit();
                    bi2.UriSource = new Uri(@"Icons/emoji.png", UriKind.Relative);
                    bi2.EndInit();
                    iconDayOne.Source = bi2;
                    break;
            }

            switch (iconNameDayTwo)
            {
                case "cloudy":
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(@"Icons/cloudy.png", UriKind.Relative);
                    bi3.EndInit();
                    iconDayTwo.Source = bi3;
                    break;
                case "rain":
                    bi3.BeginInit();

                    bi3.UriSource = new Uri(@"Icons/rain.png", UriKind.Relative);
                    bi3.EndInit();
                    iconDayTwo.Source = bi3;
                    break;
                case "snow":
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(@"Icons/snow.png", UriKind.Relative);
                    bi3.EndInit();
                    iconDayTwo.Source = bi3;
                    break;
                case "wind":
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(@"Icons/wind.png", UriKind.Relative);
                    bi3.EndInit();
                    iconDayTwo.Source = bi3;
                    break;
                default:
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(@"Icons/emoji.png", UriKind.Relative);
                    bi3.EndInit();
                    iconDayTwo.Source = bi3;
                    break;
            }

            switch (iconNameDayThree)
            {
                case "cloudy":
                    bi4.BeginInit();
                    bi4.UriSource = new Uri(@"Icons/cloudy.png", UriKind.Relative);
                    bi4.EndInit();
                    iconDayThree.Source = bi4;
                    break;
                case "rain":
                    bi4.BeginInit();
                    bi4.UriSource = new Uri(@"Icons/rain.png", UriKind.Relative);
                    bi4.EndInit();
                    iconDayThree.Source = bi4;
                    break;
                case "snow":
                    bi4.BeginInit();
                    bi4.UriSource = new Uri(@"Icons/snow.png", UriKind.Relative);
                    bi4.EndInit();
                    iconDayThree.Source = bi4;
                    break;
                case "wind":
                    bi4.BeginInit();
                    bi4.UriSource = new Uri(@"Icons/wind.png", UriKind.Relative);
                    bi4.EndInit();
                    iconDayThree.Source = bi4;
                    break;
                default:
                    bi4.BeginInit();
                    bi4.UriSource = new Uri(@"Icons/emoji.png", UriKind.Relative);
                    bi4.EndInit();
                    iconDayThree.Source = bi4;
                    break;
            }
        }

    }
}
