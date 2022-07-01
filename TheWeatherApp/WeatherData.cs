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

        public string Description { get; set; }

        public string Temperatur { get; set; }

        public string TemperaturDayOne { get; set; }

        public string TemperaturDayTwo { get; set; }

        public string TemperaturDayThree { get; set; }

        public string Wind { get; set; }

    }
}
