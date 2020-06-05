using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oldrook.Models
{
    public class Celeb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(48)]
        public string Fullname { get; set; }

        //[Required]
        //public Guid PId { get; set; }

        public Byte[] Picture { get; set; }

        public string ContentTye { get; set; }
    }
}
