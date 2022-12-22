using RisingJoker.BaseGameObjects;

namespace RisingJoker.Mediator
{
    public interface IMediator
    {
        void React(IGameObject sender, IGameObject touchedObject);
    }
}
