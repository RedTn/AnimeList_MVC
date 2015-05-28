using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        //Enables AnimeList/Explore to display SeriesType as String
        [JsonConverter(typeof(StringEnumConverter))]
        public SeriesType SeriesType { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Episodes { get; set; }

        [Range(0.0, 10.0, ErrorMessage="Please enter a demical number between 0.0 and 10.0")]
        public decimal Score { get; set; }

        [DataType(DataType.Upload)]
        [NotMapped]
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageUpload { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public static string[] validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png",
                "image/jpg"
            };
    }
}