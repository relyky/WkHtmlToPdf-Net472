using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet.Contracts;

namespace WkHtmlToPdfDotNet.EventDefinitions
{
    public class PhaseChangedArgs : EventArgs
    {
        public IDocument Document { get; set; }

        public int PhaseCount { get; set; }

        public int CurrentPhase { get; set; }

        public string Description { get; set; }
    }
}
