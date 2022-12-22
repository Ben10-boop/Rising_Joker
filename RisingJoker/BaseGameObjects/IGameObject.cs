using System;
using System.Drawing;

namespace RisingJoker.BaseGameObjects
{
    public interface IGameObject
    {
        IGameObjectInfo info { get; set; }
        Point position { get; set; }

        event EventHandler ObjectDestruction;

        void ChangeColor(Color color);
        Rectangle GetBounds();
        bool IsCollidingWith(IGameObject other);
        void OnCollisionWith(IGameObject other);
        void Render();
        bool IsObjectAlive();
        void RemoveFromScreen();
    }
}