using RisingJoker.BaseGameObjects;

namespace RisingJoker.PlatformFactory
{
    public interface IEnemy : IMovableObject, ICloneable<IEnemy>, IPoints
    {
    }
}