using System;
using System.Collections.Generic;
using Common.Utility;
using UnityEngine.Events;

namespace Common.Animations
{
    [Serializable]
    public struct AnimationEventContainer
    {
        public AnimationEventObject AnimationEvent;
        public List<UnityEvent> Events;

        public void Setup()
        {
            AnimationEvent.EnterEvent += EnterEvents;
            AnimationEvent.ExitEvent += ExitEvents;
        }

        public void SetDown()
        {
            AnimationEvent.Cleanup();
        }

        private void EnterEvents(int[] indicies)
        {
            foreach (int index in indicies)
            {
                AnimateEventByIndex(index);
            }
        }

        private void ExitEvents(int[] indicies)
        {
            foreach (int index in indicies)
            {
                AnimateEventByIndex(index);
            }
        }

        public void AnimateEventByIndex(int index)
        {
            Events[index]?.Invoke();
        }
    }
}