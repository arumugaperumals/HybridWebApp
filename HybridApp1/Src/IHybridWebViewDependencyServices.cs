using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridApp1
{
    /// <summary>
    /// An interface to represent dependency services for <see cref="HybridWebView"/>.
    /// </summary>
    public interface IHybridWebViewDependencyServices
    {
        void SetLabel(object nativeObject, string value);
    }
}
