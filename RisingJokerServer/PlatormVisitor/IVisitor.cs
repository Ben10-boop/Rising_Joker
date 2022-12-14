using RisingJokerServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.PlatormVisitor
{
    internal interface IVisitor
    {
        void visitPlatform(PlatformDto platform);

        void visitPlayerPosition(PlayerPositionDto playerPosition);
    }
}
