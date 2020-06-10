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

        [Required]
        [Range(0, 4)]
        public byte Sex { get; set; }

        public Celeb BestFriend { get; set; }

        //[Required]
        //public Guid PId { get; set; }

        #region Image

        public byte[] Image { get; set; }

        public string ContentType { get; set; }

        public string GetInlineImageSrc()
        {
            if (Image == null || ContentType == null)
                return null;

            var base64Image = System.Convert.ToBase64String(Image);
            return $"data:{ContentType};base64,{base64Image}";
        }

        public void SetImage(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null)
                return;

            ContentType = file.ContentType;

            using (var stream = new System.IO.MemoryStream())
            {
                file.CopyTo(stream);
                Image = stream.ToArray();
            }
        }

        #endregion
    }
}
