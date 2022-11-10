using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.LobbyCommand
{
    public interface ICommand
    {
        void SelectPlayerType(PlayerColor color);
        void UndoSelection(PlayerColor color);
    }
}
