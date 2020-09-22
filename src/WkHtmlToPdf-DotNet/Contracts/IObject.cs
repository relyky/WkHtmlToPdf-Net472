using System;
using System.Collections.Generic;
using System.Linq;

namespace WkHtmlToPdfDotNet.Contracts
{
    public interface IObject : ISettings
    {
        byte[] GetContent();
    }
}
