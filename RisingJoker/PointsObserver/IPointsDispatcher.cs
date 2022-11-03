using RisingJoker.PlatformFactory;

namespace RisingJoker.PointsObserver
{
    public interface IPointsDispatcher : IPoints
    {
        void Subscribe(IPointsListener listener);
        void Unsubscribe(IPointsListener listener);
    }
}
