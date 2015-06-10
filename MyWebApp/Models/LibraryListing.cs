using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApp.Models
{
    public enum LibraryStatus
    {
        Watching, 
        Completed,
        [EnumMember(Value = "On Hold")]
        OnHold,
        Dropped, 
        PlanToWatch
    }

    public class LibraryListing
    {
        [Required]
        [Key]
        [Column(Order = 0)]
        public int AnimeListId { get; set; }
        public virtual AnimeList AnimeList { get; set; }

        public decimal MyScore { get; set; }

        public int Progress { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LibraryStatus LibraryStatus { get; set; }

        [Required]
        [Key]
        [Column(Order = 1)]
        public int AnimeAccountId { get; set; }
        public virtual AnimeAccount AnimeAccount { get; set; }
    }
}