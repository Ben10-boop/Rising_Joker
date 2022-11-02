using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatGenerationStrategy
{
    internal class PlatGenerationContext
    {
        IPlatGenerationStrategy Strategy;

        public void SetStrategy(IPlatGenerationStrategy strat)
        {
            Strategy = strat;
        }

        public PlatformDto GeneratePlatform()
        {
            if (Strategy == null)
                throw new Exception("No strategy was set!");

            return Strategy.GeneratePlatform();
        }
    }
}
