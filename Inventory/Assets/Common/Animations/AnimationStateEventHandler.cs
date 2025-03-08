using System;
using System.Collections.Generic;
using Common.Animations;
using UnityEngine;

namespace Common.Utility
{
    public class AnimationStateEventHandler : StateMachineBehaviour
    {
        private AnimationEventHandler _handler;

        [SerializeField] private int[] _enterEvents;
        [SerializeField] private int[] _exitEvents;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!_handler) _handler = animator.GetComponent<AnimationEventHandler>();
            if (_enterEvents == null) return;
            _handler.EnterEvents(_enterEvents);
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_exitEvents == null) return;
            _handler.ExitEvents(_exitEvents);
        }
    }
}