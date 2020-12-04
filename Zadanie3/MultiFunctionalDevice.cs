using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public class MultiFunctionalDevice : Copier
    {
        private IFax _fax;
        public MultiFunctionalDevice(IPrinter printer, IScanner scanner, IFax fax) : base(printer, scanner)
        {
            this._fax = fax;
        }

        public int SendCounter { get; private set; }

        public void FaxOn()
        {
            this._fax.PowerOn();
        }

        public void FaxOff()
        {
            this._fax.PowerOff();
        }

        public void Send(IDocument doc, string faxAddress)
        {
            if(GetState() == IDevice.State.on)
            {
                FaxOn();
                _fax.Send(doc, faxAddress);
                SendCounter++;
                FaxOff();
            }
        }

        public void ScanAndSend(string faxAddress)
        {
            if(GetState() == IDevice.State.on)
            {
                ScannerOn();
                this._scanner.Scan(out IDocument doc, IDocument.FormatType.JPG);
                ScanCounter++;
                ScannerOff();

                FaxOn();
                this._fax.Send(doc, faxAddress);
                SendCounter++;
                FaxOff();
            }
        }
    }
}
