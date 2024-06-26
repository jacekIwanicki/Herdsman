using HerdsmanProject.Common;
using Movement;
using StateMachineSystem;

namespace HerdsmanProject.Animals.States
{
    public class FollowOwnerState : IState
    {
        private readonly IMovementController movementController;

        private HerdOwner owner;

        public FollowOwnerState(IMovementController movementController)
        {
            this.movementController = movementController;
        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }

        public void Tick()
        {
            if (owner == null)
            {
                return;
            }

            movementController.SetTargetPosition(owner.transform.position);
        }

        public void SetOwner(HerdOwner herdOwner)
        {
            owner = herdOwner;
        }
    }
}