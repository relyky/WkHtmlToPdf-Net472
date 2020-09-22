using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet.Contracts;

namespace WkHtmlToPdfDotNet
{
    public class HtmlToPdfDocument : IDocument
    {
        public List<ObjectSettings> Objects { get;  private set;}
        
        private GlobalSettings globalSettings = new GlobalSettings();

        public GlobalSettings GlobalSettings {
            get { return globalSettings; }
            set {
                globalSettings = value;
            }
        }

        public HtmlToPdfDocument()
        {
            Objects = new List<ObjectSettings>();
        }

        public IEnumerable<IObject> GetObjects()
        {
            return Objects.ToArray();
        }   
    }
}
