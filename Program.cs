using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("[INFO] Usage: <folder_path> <extension>");
            return;
        }

        if (!Directory.Exists(args[0]))
        {
            return;
        }

        string format = args[1];

        string[] imagePaths = Directory.GetFiles(args[0], format);

        string pdfFilePath = Path.Combine(args[0], "output.pdf"); // Change this to your desired output path

        Console.WriteLine($"Starting PDF compilation of {imagePaths.Length} ");

        using (PdfDocument document = new())
        {
            foreach (string imagePath in imagePaths)
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XImage image = XImage.FromFile(imagePath);

                double width = page.Width;
                double height = page.Height;

                gfx.DrawImage(image, 0, 0, width, height);
            }

            document.Save(pdfFilePath);
        }

        Console.WriteLine($"PDF created successfully at: {pdfFilePath}");
    }
}
