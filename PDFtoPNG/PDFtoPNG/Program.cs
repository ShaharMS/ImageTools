using Apitron.PDF.Rasterizer;
using Apitron.PDF.Rasterizer.Configuration;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
#pragma warning disable CA1416 // Validate platform compatibility
namespace PDFtoPNG
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int HIDE = 0;
        const int SHOW = 5;

        IntPtr CurrentWindow = GetConsoleWindow();
        static void Main(string[] args)
        {
            Console.WriteLine("ight imma convert to PNG");
            //checks if the file Path.txt is present, else create it
            File.AppendText("Path.txt").Close();

            //checks if the file RunDetails is present, else create it
            var stream = File.AppendText("ConvertionFinished.txt");
            //if the program finshied the convertion. will always be on the first line
            stream.Write("false");
            stream.Close();
            //the amount of PDF pages, the same as the amount of images being converted
            stream = File.AppendText("PageCount.txt");
            stream.Write("0");
            stream.Close();
            //exit code:
            // 0 - success
            // 1 - fail
            // 2 - never started
            //whenever you convert a file again the value will be set to 2, and then will be set to either 1\0 depending on the outcome
            stream = File.AppendText("ExitCode.txt");
            stream.WriteLine("2");
            stream.Close();
            //actually read the content
            var pdfpath = File.ReadAllText("Path.txt");
            var savePath = Environment.CurrentDirectory;

            // open and load the file
            if (pdfpath == "") return;
            try
            {
                using (FileStream fs = new FileStream(pdfpath, FileMode.Open))
                {
                    var document = new Document(fs);
                    // process and save pages one by one


                    for (int i = 0; i < document.Pages.Count; i++)
                    {

                        Page currentPage = document.Pages[i];
                        byte[] ByteArray = currentPage.RenderAsBytes((int)currentPage.Width, (int)currentPage.Height, new RenderingSettings());
                        using (Bitmap output = new Bitmap((int)currentPage.Width, (int)currentPage.Height))
                        {
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, output.Width, output.Height);
                            BitmapData bmpData = output.LockBits(rect, ImageLockMode.ReadWrite, output.PixelFormat);
                            IntPtr ptr = bmpData.Scan0;
                            Marshal.Copy(ByteArray, 0, ptr, ByteArray.Length);
                            output.UnlockBits(bmpData);
                            File.Create(savePath + $"\\{i + 1}.png").Close();
                            output.Save(savePath + $"\\{i + 1}.png");
                        }


                    }
                    File.WriteAllText("ConvertionFinished.txt", "true");
                    File.WriteAllText("PageCount.txt", $"{document.Pages.Count}");
                    File.WriteAllText("ExitCode.txt", "0");
                }
            } catch(Exception e)
            {
                //keeps last convertion data
                File.WriteAllText("ExitCode.txt", "1");
            }
            
        }
    }
}
