using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace Dataway.Extensions.External
{
    public class Weather
    {
        #region Public Properties

        public TodayWeatherDay Today { get; set; }

        public List<WeatherDay> Forcast { get; set; }

        #endregion

        #region Public Static Methods

        public static Weather Get(string state, string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return null;
            }

            var weather = new Weather();

            string xml;
            string url = string.Format("http://rss.weather.com.au/{0}/{1}", state, city);
            using (var wc = new WebClient())
            {
                xml = wc.DownloadString(url);
            }

            XDocument xmlDoc = XDocument.Parse(xml);

            // Forcast
            weather.Forcast = new List<WeatherDay>();
            XName forecastQualifiedName = XName.Get("forecast", "http://rss.weather.com.au/w.dtd");
            var forcast = xmlDoc.Descendants(forecastQualifiedName);
            foreach (var forcastItem in forcast)
            {
                WeatherDay day = new WeatherDay();
                if (forcastItem == forcast.First())
                {
                    day = new TodayWeatherDay();
                }

                day.MinTemperature = float.Parse(forcastItem.Attribute("min").Value);
                day.MaxTemperature = float.Parse(forcastItem.Attribute("max").Value);
                day.Description = forcastItem.Attribute("description").Value;
                day.Icon = int.Parse(forcastItem.Attribute("icon").Value);
                day.ShortDescription = forcastItem.Attribute("iconAlt").Value;
                weather.Forcast.Add(day);
            }

            // Today
            XName currentTemperatureXName = XName.Get("current", "http://rss.weather.com.au/w.dtd");
            var current = xmlDoc.Descendants(currentTemperatureXName);
            weather.Today = weather.Forcast.First() as TodayWeatherDay;
            weather.Today.CurrentTemperature = float.Parse(current.First().Attribute("temperature").Value);
            weather.Today.DewPoint = float.Parse(current.First().Attribute("dewPoint").Value);
            weather.Today.Humidity = float.Parse(current.First().Attribute("humidity").Value);
            weather.Today.WindSpeed = float.Parse(current.First().Attribute("windSpeed").Value);
            weather.Today.WindGusts = float.Parse(current.First().Attribute("windGusts").Value);
            weather.Today.WindDirection = current.First().Attribute("windDirection").Value;
            weather.Today.Pressure = float.Parse(current.First().Attribute("pressure").Value);
            weather.Today.Rain = float.Parse(current.First().Attribute("rain").Value);

            return weather;
        }

        #endregion

    }

    public class WeatherDay
    {
        #region Public Properties

        public float MinTemperature { get; set; }

        public float MaxTemperature { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int Icon { get; set; }

        #endregion
    }

    public class TodayWeatherDay : WeatherDay
    {
        #region Public Properties

        public float CurrentTemperature { get; set; }

        public float DewPoint { get; set; }

        public float Humidity { get; set; }

        public float WindSpeed { get; set; }

        public float WindGusts { get; set; }

        public string WindDirection { get; set; }

        public float Pressure { get; set; }

        public float Rain { get; set; }

        #endregion

    }
}