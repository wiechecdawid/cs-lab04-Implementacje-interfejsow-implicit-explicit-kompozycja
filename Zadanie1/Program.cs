using System;

namespace ver1
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);

            Console.WriteLine(doc2.GetFileName());

            xerox.PowerOff();
            xerox.Scan(out doc2);
            Console.WriteLine(doc2.GetFileName());
        }
    }
}
