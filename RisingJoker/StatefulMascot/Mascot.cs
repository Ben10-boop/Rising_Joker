using RisingJoker.BaseGameObjects;
using System.Drawing;

namespace RisingJoker.StatefulMascot
{
    public class Mascot : MovableObject
    {
        public static string TAG = "mascot";
        private MascotState state;
        private int RenderCounter;

        public Mascot(Size size, Point position, Color color, Player parentPlayer) : base(size, position, true, color, TAG)
        {
            state = new IdleMascotState(parentPlayer, this);
        }

        public void ChangeState(MascotState state)
        {
            this.state = state;
        }

        public override void OnCollisionWith(IGameObject other)
        {
            state.OnCollisionWith(other);
        }

        public override void Move()
        {
            state.Move();
        }

        public override void Render()
        {
            RenderCounter++;

            if (RenderCounter >= 100)
            {
                RenderCounter = 0;
                state.RandomizeNextState();
            }

            base.Render();
        }
    }
}
