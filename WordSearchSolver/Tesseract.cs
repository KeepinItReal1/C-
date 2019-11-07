using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Patagames.Ocr;
using Patagames.Ocr.Enums;

namespace WordSearchSolver
{
    public class Tesseract
    {
        public static string ConvertImageToText(string filePath)
        {/*C:\Users\justas\Desktop\Blockchain\WordSearchSolver\WordSearchSolver*/
            /*C:\Users\justas\Desktop\Blockchain\WordSearchSolver\WordSearchSolver\bin\Debug\netcoreapp3.0*/
            string plainText = "Error 505";
            using (var api = OcrApi.Create())
            {
                api.Init(Languages.Lithuanian,"./");
                plainText = api.GetTextFromImage("wwwroot\\Images\\65.jpg");
            }
            return plainText;
        }
    }
}
