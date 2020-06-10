using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oldrook.Models;
using Oldrook.Services;

namespace Oldrook.Pages.Admin
{
    public class AddEditCelebModel : PageModel
    {
        private readonly ICelebService celebService;

        [FromRoute]
        public int? Id { get; set; }

        [BindProperty]
        public Celeb Celeb { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public bool IsNewOne
        {
            get { return Id == null; }
        }

        public AddEditCelebModel(ICelebService celebService)
        {
            this.celebService = celebService;
        }
        public async Task OnGetAsync()
        {
            Celeb = await celebService.FindAsync(Id.GetValueOrDefault());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Celeb.Id = Id.GetValueOrDefault();
            var celeb = await celebService.FindAsync(Id.GetValueOrDefault())
                ?? new Celeb();

            //mapping
            celeb.Fullname = Celeb.Fullname;
            if (Image != null)
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    celeb.Image = stream.ToArray();
                    celeb.ContentType = Image.ContentType;
                }
            }

            await celebService.SaveAsync(celeb);
            return RedirectToPage("/Admin/Celeb_", new { id = celeb.Id });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await celebService.DeleteAsync(Id.Value);
            return RedirectToPage("/Admin/Celebs");
        }

    }
}
