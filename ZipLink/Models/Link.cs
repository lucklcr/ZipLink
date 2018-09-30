using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZipLink.Models
{
    [Table("links")]
    public class Link
    {
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100)]
        public string Description { get; set; }

        public int Visits { get; set; }
        public DateTime? LastVisit { get; set; }
        public bool Enabled { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
    }
}