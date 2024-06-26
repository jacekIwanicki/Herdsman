using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rb2DMovementController : MonoBehaviour, IMovementController
    {
        [SerializeField]
        private float speed = 5f;
        [SerializeField]
        private float destinationReachedRadius = 0.5f;

        private Vector2 targetPosition;
        private bool isMoving;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!isMoving)
            {
                return;
            }

            if (Vector2.Distance(transform.position, targetPosition) > destinationReachedRadius)
            {
                MoveToTarget();
            }
            else
            {
                isMoving = false;
            }
        }
        public void Stop()
        {
            isMoving = false;
        }

        public void SetTargetPosition(Vector2 position)
        {
            isMoving = true;
            targetPosition = position;
        }

        private void MoveToTarget()
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            rb.MovePosition(newPosition);
        }

        public Vector2 GetPosition()
        {
            return rb.transform.position;
        }
    }
}