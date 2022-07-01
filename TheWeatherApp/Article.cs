using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWeatherApp
{
    class Article
    {
        public string Title { get; set; }

        public override string ToString()
        {
            return " " + this.Title;
        }
    }
}
