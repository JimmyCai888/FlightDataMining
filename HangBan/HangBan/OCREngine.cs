using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tesseract;
using System.IO;

namespace HangBan
{
    public class OCREngine
    {
        public static string GetOCRText(string engpath, Image imgdata)
        {
            string rst = "";

            try
            {
                Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));
                using (var engine = new TesseractEngine(engpath, "eng", EngineMode.Default))
                {
                    using (Bitmap image = new Bitmap(imgdata))
                    {
                        using (var pix = PixConverter.ToPix(image))
                        {
                            using (var page = engine.Process(pix))
                            {
                                rst = page.GetText().Trim();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ScratchModel.WriteLogFile("----Error----", "------ GetOCRText1 -------", "");
            	
            }

            return rst;
        }
    }
}
