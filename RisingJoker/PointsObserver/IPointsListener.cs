using RisingJoker.PlatformFactory;

namespace RisingJoker.PointsObserver
{
    public interface IPointsListener : IPoints
    {
        void Update(int points, string id);
    }
}
