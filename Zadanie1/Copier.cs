using System;
using System.Collections.Generic;
using System.Text;

namespace ver1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Today} Print: {document.GetFileName()}");
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            string sType;

            switch(formatType)
            {
                case IDocument.FormatType.TXT:
                    sType = "Text";
                    break;
                case IDocument.FormatType.PDF:
                    sType = "PDF";
                    break;
                default:
                    sType = "Image";
                    break;
            }

            string name = string.Format("{0}Scan{1}.{2}", sType, ScanCounter+1, formatType.ToString().ToLower());

            if (formatType == IDocument.FormatType.PDF)
                document = new PDFDocument(name);
            if (formatType == IDocument.FormatType.JPG)
                document = new ImageDocument(name);
            else
                document = new TextDocument(name);

            if (state == IDevice.State.on)
            {
                ScanCounter++;
                Console.WriteLine($"{DateTime.Today} Scan: {document.GetFileName()}");
            }
        }

        public void ScanAndPrint()
        {
            Scan(out IDocument doc);
            Print(doc);
        }
    }
}
