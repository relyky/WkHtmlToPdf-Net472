using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet.Contracts;

namespace WkHtmlToPdfDotNet.EventDefinitions
{
    public class ProgressChangedArgs : EventArgs
    {
        public IDocument Document { get; set; }

        public string Description { get; set; }
    }
}
