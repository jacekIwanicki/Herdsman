using UnityEngine;

namespace Movement
{
    public interface IMovementController
    {
        public void SetTargetPosition(Vector2 position);
        public void Stop();
        public Vector2 GetPosition();
    }
}