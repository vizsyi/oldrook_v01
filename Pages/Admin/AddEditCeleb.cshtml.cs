using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oldrook.Models;

namespace Oldrook.Pages.Admin
{
    public class AddEditCelebModel : PageModel
    {
        [FromRoute]
        public int? Id { get; set; }

        public  Celeb Celeb { get; set; }

        public bool IsNewOne
        {
            get { return Id == null; }
        }
        public void OnGet()
        {
            //var i = long.Parse ((string)RouteData.Values["id"]);
        }
    }
}
