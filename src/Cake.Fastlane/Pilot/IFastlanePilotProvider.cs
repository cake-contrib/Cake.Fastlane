using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Fastlane
{
    internal interface IFastlanePilotProvider
    {
        /// <summary>
        /// Executes fastlane pilot with the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        void Pilot(FastlanePilotConfiguration configuration);
    }
}
