﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventSearch.Models
{
    public class CommentsTable
    {
        [Key]
        public int CommentId { get; set; }

        public string Comment { get; set; }

        public DateTime CommentDate { get; set; }

        [ForeignKey("AdventurePost")]
        public int AdventurePostId { get; set; }
        public AdventurePost AdventurePost { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
