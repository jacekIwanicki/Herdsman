using System;

namespace StateMachineSystem
{
    public class Transition
    {
        public Func<bool> Condition { get; }
        public IState TransitionState { get; }
        public bool CanTransitionToSelf { get; }

        public Transition(IState to, Func<bool> condition, bool canTransitionToSelf = false)
        {
            TransitionState = to;
            Condition = condition;
            CanTransitionToSelf = canTransitionToSelf;
        }
    }
}