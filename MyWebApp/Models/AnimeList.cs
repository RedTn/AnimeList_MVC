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
        public SeriesType SeriesType { get; set; }
        public int Episodes { get; set; }
        public decimal Score { get; set; }
    }
}