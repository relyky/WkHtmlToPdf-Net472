using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WkHtmlToPdfDotNet.Contracts
{
    public interface IDocument : ISettings
    {
       IEnumerable<IObject> GetObjects();
    }
}
