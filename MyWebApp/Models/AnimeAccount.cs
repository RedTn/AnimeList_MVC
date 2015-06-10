using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public class AnimeAccount
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        [RegularExpression(@"\w{3,15}", ErrorMessage="Username must be between 3 and 15 characters.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        //Reference to the User that holds this account, 
        //automatically implemented with a foreign key that references the user table when database generated.
        //Making it virtual allows it to be overriden by framework, using a mechanism that supports lazy loading of this related object
        public virtual ApplicationUser User { get; set; }

        //Entity is smart enough to know we are talking about Id property of the related ApplicationUser property
        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ICollection<LibraryListing> LibraryListings { get; set; }
    }
}