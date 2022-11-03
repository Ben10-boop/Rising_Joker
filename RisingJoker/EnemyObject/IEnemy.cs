using RisingJoker.BaseGameObjects;
using RisingJoker.PointsObserver;

namespace RisingJoker.PlatformFactory
{
    public interface IEnemy : IMovableObject, ICloneable<IEnemy>, IPointsDispatcher
    {
    }
}