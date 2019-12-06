using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WordSearchSolver.Pages
{
    public class SolverModel : PageModel
    {
        public IActionResult OnPost()
        {
            var input = Request.Form["word"];
            Puzzle.findWord(input);
            return Page();
        }
    }
}