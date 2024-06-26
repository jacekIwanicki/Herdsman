using System;
using System.Collections.Generic;

namespace StateMachineSystem
{
    public class StateMachine
    {
        private static readonly List<Transition> EmptyTransitions = new List<Transition>(0);
        private readonly Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
        private readonly List<Transition> anyTransitions = new List<Transition>();
        private List<Transition> currentTransitions = new List<Transition>();
        private IState currentState;

        public void Tick()
        {
            Transition transition = GetTransition();
            if (transition != null)
            {
                SetState(transition.TransitionState);
            }

            currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if (state == currentState) return;

            currentState?.OnExit();
            currentState = state;

            transitions.TryGetValue(currentState.GetType(), out currentTransitions);

            if (currentTransitions == null)
            {
                currentTransitions = EmptyTransitions;
            }

            currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (this.transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                this.transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (Transition transition in anyTransitions)
            {
                if (transition.Condition() && transition.CanTransitionToSelf == false && transition.TransitionState != currentState)
                {
                    return transition;
                }
            }

            foreach (Transition transition in currentTransitions)
            {
                if (transition.Condition() && transition.CanTransitionToSelf == false && transition.TransitionState != currentState)
                {
                    return transition;
                }
            }

            return null;
        }
    }
}