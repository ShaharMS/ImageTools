using System;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace ImageProcessing
{
    class Program
    {
        public static string ProcessType = "";
        public static string ImagePath = "";

        static void Main(string[] args)
        {
            ProcessType = args[0] != null ? args[0] : throw new Exception("Image Processing Error: Processing Type Not Found/Supplied");
            ImagePath = args[1] != null ? args[1] : throw new Exception("Image Processing Error: Image Path Not Found/Supplied");        
            switch (ProcessType)
            {
                case "DetectLines":
                {
                    Image < Bgr, Byte> image = new Image<Bgr, Byte>(ImagePath);
                    var vectors = new VectorOfPointF();
                    CvInvoke.HoughLines(image, vectors, 0, 0, 40);
                    Console.WriteLine(vectors.ToString());
                    return;
                }
                case "DetectText":
                {
                    break;
                }
            }
        }
    }
}
