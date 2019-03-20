using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventSearch.Models
{
    public class ApplicationUser
    {
        [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isAdmin { get; set; }
        public bool isUser { get; set; }
    }
}
