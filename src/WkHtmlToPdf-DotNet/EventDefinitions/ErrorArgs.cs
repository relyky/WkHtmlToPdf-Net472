using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet.Contracts;

namespace WkHtmlToPdfDotNet.EventDefinitions
{
    public class ErrorArgs : EventArgs
    {
        public IDocument Document { get; set; }

        public string Message { get; set; }
    }
}
