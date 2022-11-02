using System;
using System.Drawing;

namespace RisingJoker.Object
{
    public interface IGameObject
    {
        Color color { get; set; }
        bool isVisible { get; set; }
        string objectTag { get; set; }
        Point position { get; set; }
        Size size { get; set; }

        event EventHandler ObjectDestruction;

        void ChangeColor(Color color);
        void CreateObject();
        Rectangle GetBounds();
        bool IsCollidingWith(IGameObject other);
        void OnCollisionWith(IGameObject other);
        void Render();
    }
}