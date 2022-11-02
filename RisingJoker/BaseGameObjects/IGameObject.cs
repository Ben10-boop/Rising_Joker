using System;
using System.Drawing;

namespace RisingJoker.BaseGameObjects
{
    public interface IGameObject
    {
        string objectTag { get; set; }
        Point position { get; set; }
        Size size { get; set; }

        event EventHandler ObjectDestruction;

        void ChangeColor(Color color);
        Rectangle GetBounds();
        bool IsCollidingWith(IGameObject other);
        void OnCollisionWith(IGameObject other);
        void Render();
        bool IsInScreen();
        void RemoveFromScreen();
    }
}