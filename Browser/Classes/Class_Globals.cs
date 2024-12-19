using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WV2Service;
namespace MobileView
{
    public partial class Globals
    {
        public static class Instances
        {
            public static readonly WebViewService Browser = new WebViewService();
        }
    }
}
