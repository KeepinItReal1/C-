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
        private string Folder { get; } = "wwwroot\\Images\\";
        private string ImageName { get; set; } = "image.jpg";
        public string ErrorMessage { get; set; }
        public IActionResult OnPost()
        {
            var file = Path.Combine(_environment.ContentRootPath, Folder, ImageName);
            string imageType = Upload.ContentType.Split('/').Last();
            if (imageType == Puzzle.imageType)
            {
                var fileStream = new FileStream(file, FileMode.Create, FileAccess.ReadWrite);
                Upload.CopyTo(fileStream);
                Puzzle.imagePath = Folder + ImageName;
                Puzzle.initializePuzzle();
                return RedirectToPage("Solver");
            }
            else 
            {
                ErrorMessage = "Invalid file type. Only *." + Puzzle.imageType + " files are allowed" + imageType;
                return Page();
            }
        }
    }
}
