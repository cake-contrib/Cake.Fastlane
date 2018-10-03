using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Fastlane
{
    internal interface IFastlaneSupplyProvider
    {
        /// <summary>
        /// Executes fastlane supply with the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        void Supply(FastlaneSupplyConfiguration configuration);
    }
}
