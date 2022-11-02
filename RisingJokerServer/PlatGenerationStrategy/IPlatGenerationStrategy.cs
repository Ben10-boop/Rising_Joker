using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatGenerationStrategy
{
    internal interface IPlatGenerationStrategy
    {
        /// <summary>
        /// Generates a random set of data for creating either a platform or a platform array
        /// </summary>
        /// <returns></returns>
        PlatformDto GeneratePlatform();
    }
}
