using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.LobbyCommand
{
    public class Invoker
    {
        private ICommand selectType;
        private ICommand undoPlayerType;

        public void SelectPlayerType(ICommand command)
        {
            this.selectType = command;
        }

        public void UndoPlayerType(ICommand command)
        {
            this.undoPlayerType = command;
        }

        public void SelectOrUndoPlayer(PlayerColor color)
        {
            if (this.selectType is ICommand)
            {
                this.selectType.SelectPlayerType(color);
            }

            if (this.undoPlayerType is ICommand)
            {
                this.undoPlayerType.UndoSelection(color);
            }
        }
    }
}
