using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace Zadanie2
{
    public class MultiFunctionalDevice : BaseDevice, IPrinter, IScanner, IFax
    {
        public MultiFunctionalDevice(string faxNumber)
        {
            FaxNumber = faxNumber;
        }

        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public int SendCounter { get; private set; } = 0;

        public string FaxNumber { get; }

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

            switch (formatType)
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

            string name = string.Format("{0}Scan{1}.{2}", sType, ScanCounter + 1, formatType.ToString().ToLower());

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

        public void Send(IDocument document, string faxNumber)
        {
            if(state == IDevice.State.on)
            {
                SendCounter++;
                Console.WriteLine($"{DateTime.Today} Sent: {document.GetFileName()} from: {this.FaxNumber} to: {faxNumber}");
            }
        }

        public void ScanAndSend(string faxNumber)
        {
            Scan(out IDocument doc);
            Send(doc, faxNumber);
        }
    }
}
