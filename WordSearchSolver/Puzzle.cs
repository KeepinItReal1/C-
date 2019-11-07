using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordSearchSolver
{
    public static class Puzzle
    {
        public static string imagePath { get; set; }
        public static string imagePathHTML { get; set; }
        public static string imageType { get; } = "jpeg";
        public static string text { get; set; }

        public static void initializePuzzle() {
            text = Tesseract.ConvertImageToText(imagePath);
        }
    }
}
