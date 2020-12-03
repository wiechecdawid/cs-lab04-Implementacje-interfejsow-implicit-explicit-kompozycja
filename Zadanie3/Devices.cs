using System;

namespace Zadanie3
{
    public interface IDevice
    {
        enum State {on, off};

        void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
        void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
        State GetState(); // zwraca aktualny stan urządzenia

        int Counter {get;}  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                            // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public abstract class BaseDevice : IDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            state = IDevice.State.off;
            Console.WriteLine("... {0} is off !", this.GetType().Name);
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
                Counter++;

            state = IDevice.State.on;
            Console.WriteLine("{0} is on ...", this.GetType().Name);  
        }

        public int Counter { get; private set; } = 0;
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }


    public class Printer : BaseDevice, IPrinter
    { 
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
                Console.WriteLine($"{DateTime.Today} Print: {document.GetFileName()}");
        }
    }

    public class Scanner : BaseDevice, IScanner
    {
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

            string name = string.Format("{0}Scan.{1}", sType, formatType.ToString().ToLower());

            if (formatType == IDocument.FormatType.PDF)
                document = new PDFDocument(name);
            if (formatType == IDocument.FormatType.JPG)
                document = new ImageDocument(name);
            else
                document = new TextDocument(name);

            if (state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Today} Scan: {document.GetFileName()}");
            }
        }
    }
}
