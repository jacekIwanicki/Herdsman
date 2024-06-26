using Movement;
using StateMachineSystem;
using UnityEngine;

namespace HerdsmanProject.Animals.States
{
    public class PatrollingState : IState
    {
        private readonly IMovementController movementController;

        private Vector2 targetPosition;
        private float waitTimer = 0f;
        private float wanderRadius = 4f;
        private float minWaitTime = 2f;
        private float maxWaitTime = 6f;

        private Collider2D gameAreaCollider;

        public PatrollingState(IMovementController movementController, Collider2D gameAreaCollider)
        {
            this.movementController = movementController;
            this.gameAreaCollider = gameAreaCollider;
        }

        public void OnEnter()
        {
            GenerateNewWanderTarget();
        }

        public void OnExit()
        {

        }

        public void Tick()
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= Random.Range(minWaitTime, maxWaitTime))
            {
                GenerateNewWanderTarget();
                waitTimer = 0f;
            }

            movementController.SetTargetPosition(targetPosition);
        }


        private void GenerateNewWanderTarget()
        {
            Vector2 currentPosition = movementController.GetPosition();
            Vector2 wanderOffset;
            wanderOffset = Random.insideUnitCircle * wanderRadius;
            targetPosition = currentPosition + wanderOffset;
            targetPosition.x = Mathf.Clamp(targetPosition.x, gameAreaCollider.bounds.min.x, gameAreaCollider.bounds.max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, gameAreaCollider.bounds.min.y, gameAreaCollider.bounds.max.y);
        }
    }
}