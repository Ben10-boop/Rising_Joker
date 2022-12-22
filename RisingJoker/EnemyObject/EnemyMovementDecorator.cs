using RisingJoker.BaseGameObjects;
using RisingJoker.Mediator;
using RisingJoker.PlatformFactory;
using RisingJoker.PointsObserver;
using System;
using System.Drawing;

namespace RisingJoker.EnemyObject
{
    public abstract class EnemyMovementDecorator : IEnemy
    {
        public IEnemy BaseEnemy;
        public EnemyMovementDecorator(IEnemy baseEnemy)
        {
            BaseEnemy = baseEnemy;
        }

        public int DownDirectionSpeed { get => BaseEnemy.DownDirectionSpeed; set => BaseEnemy.DownDirectionSpeed = value; }
        public int LeftDirectionSpeed { get => BaseEnemy.LeftDirectionSpeed; set => BaseEnemy.LeftDirectionSpeed = value; }
        public int RightDirectionSpeed { get => BaseEnemy.RightDirectionSpeed; set => BaseEnemy.RightDirectionSpeed = value; }
        public int UpDirectionSpeed { get => BaseEnemy.UpDirectionSpeed; set => BaseEnemy.UpDirectionSpeed = value; }
        public int ParentXEnd { get => BaseEnemy.ParentXEnd; set => BaseEnemy.ParentXEnd = value; }
        public int ParentXStart { get => BaseEnemy.ParentXStart; set => BaseEnemy.ParentXStart = value; }
        public Point position { get => BaseEnemy.position; set => BaseEnemy.position = value; }
        public int Points { get => BaseEnemy.Points; }
        public IGameObjectInfo info { get => BaseEnemy.info; set => BaseEnemy.info = value; }

        public event EventHandler ObjectDestruction
        {
            add
            {
                BaseEnemy.ObjectDestruction += value;
            }
            remove
            {
                BaseEnemy.ObjectDestruction -= value;
            }
        }

        public void ChangeColor(Color color)
        {
            BaseEnemy.ChangeColor(color);
        }

        public abstract IEnemy Clone();

        public Rectangle GetBounds()
        {
            return BaseEnemy.GetBounds();
        }

        public int GetDirectionSpeed(MoveDirection direction)
        {
            return BaseEnemy.GetDirectionSpeed(direction);
        }

        public bool IsCollidingWith(IGameObject other)
        {
            return BaseEnemy.IsCollidingWith(other);
        }

        public bool IsObjectAlive()
        {
            return BaseEnemy.IsObjectAlive();
        }

        public abstract void Move();

        public void MoveBy(Point moveBy)
        {
            BaseEnemy.MoveBy(moveBy);
        }

        public void MoveTo(Point moveTo)
        {
            BaseEnemy.MoveTo(moveTo);
        }

        public void OnCollisionWith(IGameObject other)
        {
            BaseEnemy.OnCollisionWith(other);
        }

        public void RemoveFromScreen()
        {
            BaseEnemy.RemoveFromScreen();
        }

        public void Render()
        {
            BaseEnemy.Render();
        }

        public void SetMediator(IMediator mediator)
        {
            BaseEnemy.SetMediator(mediator);
        }

        public void Subscribe(IPointsListener listener)
        {
            BaseEnemy.Subscribe(listener);
        }

        public void Unsubscribe(IPointsListener listener)
        {
            BaseEnemy.Unsubscribe(listener);
        }
    }
}
