using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tesseract;

namespace HangbanOCR
{
    public class OCREngine
    {
        public string GetOCRText(string engpath, Image imgdata)
        {
            string rst = "";
            using (var engine = new TesseractEngine(engpath, "eng", EngineMode.Default))
            {
                using (Bitmap image = new Bitmap(imgdata))
                {
                    using (var pix = PixConverter.ToPix(image))
                    {
                        using (var page = engine.Process(pix))
                        {
                            //meanConfidenceLabel.InnerText = String.Format("{0:P}", page.GetMeanConfidence());
                            rst = page.GetText().Trim();
                        }
                    }
                }
            }

            return rst;
        }
    }
}
