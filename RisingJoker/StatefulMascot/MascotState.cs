using RisingJoker.BaseGameObjects;

namespace RisingJoker.StatefulMascot
{
    public abstract class MascotState
    {
        protected Player player;
        protected Mascot mascot;

        public MascotState(Player player, Mascot mascot)
        {
            this.player = player;
            this.mascot = mascot;
        }

        public abstract void Move();
        public abstract void OnCollisionWith(IGameObject other);

        public abstract void RandomizeNextState();
    }
}
