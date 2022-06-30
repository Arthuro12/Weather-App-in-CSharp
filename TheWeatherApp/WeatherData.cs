using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TheWeatherApp
{
    public class WeatherData
    {
        public DateTime Date { get; set; }

        public string Forecast { get; set; }

        public string Description { set; get; }

        public string Temperatur { set; get; }

        public string TemperaturDayOne { set; get; }

        public string TemperaturDayTwo { set; get; }

        public string TemperaturDayThree { set; get; }

        public string Wind { set; get; }
    }
}
