using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Hosting;

namespace MyWebApp.Models
{
    public enum SeriesType
    {
        TV,
        Movie
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

        [Range(0.0, 10.0, ErrorMessage = "Please enter a demical number between 0.0 and 10.0")]
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

        [NotMapped]
        //TODO: Path literal, read that maybe putting this in config file is better
        public static string uploadDir = @"/Content/Images/AnimeList/";

        public static string FindNewFilePath(string filePath)
        {
            int copyNumber = 2;
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            string imageUrl = Path.Combine(AnimeList.uploadDir, fileName + " - Copy");
            imageUrl = Path.ChangeExtension(imageUrl, extension);
            string imagePath = HostingEnvironment.MapPath(imageUrl);

            while (System.IO.File.Exists(imagePath))
            {
                imageUrl = Path.Combine(AnimeList.uploadDir, fileName + " - Copy(" + copyNumber.ToString() + ")");
                imageUrl = Path.ChangeExtension(imageUrl, extension);
                imagePath = HostingEnvironment.MapPath(imageUrl);
                copyNumber++;
            }

            return imageUrl;
        }
    }
}