using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Patagames.Ocr;
using Patagames.Ocr.Enums;
//using Emgu.CV;
//using Emgu.CV.Structure;

namespace WordSearchSolver
{
    public class Tesseract
    {
        public static string ConvertImageToText(string filePath)
        {
            string plainText = "Error 505";
            using (var api = OcrApi.Create())
            {
                api.Init(Languages.Lithuanian, "./");
                //Image<Bgr, byte> imgInput = new Image<Bgr, byte>("..\\..\\Book_test.jpg");
                //Image<Gray, byte> imgGray = imgInput.Convert<Gray, byte>();
                //imgGray.Dilate(1);
                //imgGray.Erode(1);
                //Image<Gray, byte> imgBinarize = new Image<Gray, byte>(imgGray.Width, imgGray.Height, new Gray(0));
                //CvInvoke.Threshold(imgGray, imgBinarize, 90, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
                //plainText = api.GetTextFromImage(imgBinarize.Bitmap);
                
                plainText = api.GetTextFromImage(filePath);
            }
            return plainText;
        }
    }
}
