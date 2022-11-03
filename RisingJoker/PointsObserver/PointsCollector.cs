namespace RisingJoker.PointsObserver
{
    public class PointsCollector : IPointsListener
    {
        private readonly string PlayerId;
        public int Points { get; private set; }

        public PointsCollector(string playerId)
        {
            PlayerId = playerId;
            Points = 0;
        }

        public void Update(int points, string id)
        {
            if (id == PlayerId)
                Points += points;
        }
    }
}
