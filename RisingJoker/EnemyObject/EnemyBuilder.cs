using RisingJoker.PlatformFactory;

namespace RisingJoker.EnemyObject
{
    public class EnemyBuilder
    {
        IEnemy Enemy;
        public EnemyBuilder()
        {
            Reset();
        }

        public EnemyBuilder Reset()
        {
            return this;
        }
        public EnemyBuilder SetBaseEnemy(IEnemy enemy)
        {
            Enemy = enemy;
            return this;
        }

        public EnemyBuilder AddWalking()
        {
            Enemy = new WalkingEnemy(Enemy);
            return this;
        }

        public EnemyBuilder AddTeleporting()
        {
            Enemy = new TeleportingEnemy(Enemy);

            return this;
        }

        public EnemyBuilder AddHovering()
        {
            Enemy = new HoveringEnemy(Enemy);

            return this;
        }

        public IEnemy GetEnemy()
        {
            return Enemy;
        }
    }
}
