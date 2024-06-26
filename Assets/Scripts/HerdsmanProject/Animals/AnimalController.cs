using HerdsmanProject.Animals.States;
using HerdsmanProject.Common;
using Movement;
using StateMachineSystem;
using UnityEngine;

namespace HerdsmanProject.Animals
{
    public class AnimalController : MonoBehaviour, IAnimal
    {
        private StateMachine stateMachine = new StateMachine();
        private IGameObjectDetector gameObjectDetector;
        private IMovementController movementController;
        private FollowOwnerState followOwnerState;
        private PatrollingState patrollingState;
        private AnimalSpawner animalSpawner;
        public HerdOwner HerdOwner { get; private set; }

        private void Awake()
        {
            gameObjectDetector = GetComponent<IGameObjectDetector>();
            movementController = GetComponent<IMovementController>();
        }

        private void OnEnable()
        {
            gameObjectDetector.OnGameObjectDetected += OnGameObjectDetected;
        }

        private void OnDisable()
        {
            gameObjectDetector.OnGameObjectDetected -= OnGameObjectDetected;
        }

        private void Update()
        {
            stateMachine.Tick();
        }

        public void Initialize(AnimalSpawner animalSpawner)
        {
            this.animalSpawner = animalSpawner;
            stateMachine = new StateMachine();
            patrollingState = new PatrollingState(movementController, animalSpawner.GetAreaCollider());
            followOwnerState = new FollowOwnerState(movementController);
            stateMachine.AddTransition(patrollingState, followOwnerState, ShouldTransitionToFollowOwnerState);
            stateMachine.AddTransition(followOwnerState, patrollingState, ShouldTransitionToPatrollingState);
            stateMachine.SetState(patrollingState);
        }

        public void DespawnAnimal()
        {
            if (HerdOwner != null)
            {
                HerdOwner.RemoveFromGroup(this);
            }

            HerdOwner = null;
            stateMachine.SetState(patrollingState);
            movementController.Stop();
            animalSpawner.ReturnAnimalToPool(this);
        }

        private bool ShouldTransitionToFollowOwnerState()
        {
            return HerdOwner != null;
        }

        private bool ShouldTransitionToPatrollingState()
        {
            return HerdOwner == null;
        }

        private void OnGameObjectDetected(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out HerdOwner herdOwner) && herdOwner.TryAddToGroup(this))
            {
                HerdOwner = herdOwner;
                followOwnerState.SetOwner(herdOwner);
            }
        }
    }
}