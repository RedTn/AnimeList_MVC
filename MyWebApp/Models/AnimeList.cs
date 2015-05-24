using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public enum SeriesType
    {
        TV, Movie
    }

    public class AnimeList
    {
        public int Id { get; set; }

        public string Title { get; set; }

        //Enables AnimeList/Explore to display SeriesType as String
        [JsonConverter(typeof(StringEnumConverter))]
        public SeriesType SeriesType { get; set; }

        public int Episodes { get; set; }
        public decimal Score { get; set; }
    }
}