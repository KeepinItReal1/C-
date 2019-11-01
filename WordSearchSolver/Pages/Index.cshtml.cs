using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WordSearchSolver.Pages
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment _environment;

        public IndexModel(IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }
        public IActionResult OnPost()
        {
            var file = Path.Combine(_environment.ContentRootPath, "Images", Upload.FileName);
            var fileStream = new FileStream(file, FileMode.Create);
            Upload.CopyToAsync(fileStream);
            return RedirectToPage("Solver");
        }
    }
}
