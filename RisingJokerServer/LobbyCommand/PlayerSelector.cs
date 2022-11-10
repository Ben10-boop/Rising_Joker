using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisingJokerServer.LobbyCommand
{
    public class PlayerSelector : ICommand
    {
        private Receiver receiver;
        private PlayerColor color;

        public PlayerSelector(Receiver receiver, PlayerColor color)
        {
            this.receiver = receiver;
            this.color = color;
        }

        public void SelectPlayerType(PlayerColor color)
        {
            this.receiver.SelectColor(color);
        }

        public void UndoSelection(PlayerColor color)
        {
            this.receiver.UndoColorSelection(color);
        }
    }
}
