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

            //actually read the content
            var pdfpath = File.ReadAllText("Path.txt");

            // open and load the file
            if (pdfpath == "") return;
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
                        File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\{i}.png").Close();
                        output.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\{i}.png");
                    }
                    
                    
                }
            }
        }
    }
}
