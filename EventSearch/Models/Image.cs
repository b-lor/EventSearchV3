using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventSearch.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }

        public string Caption { get; set; }

        public string ImagePath { get; set; }
    }
}
