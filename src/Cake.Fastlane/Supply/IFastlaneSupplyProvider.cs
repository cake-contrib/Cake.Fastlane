using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Fastlane
{
    internal interface IFastlaneSupplyProvider
    {
        void Supply(FastlaneSupplyConfiguration configuration);
    }
}
