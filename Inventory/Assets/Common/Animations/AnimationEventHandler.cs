using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Animations
{
    public class AnimationEventHandler : MonoBehaviour
    {
        [SerializeField] private List<UnityEvent> _enterEvent;
        [SerializeField] private List<UnityEvent> _exitEvent;

        public void EnterEvents(int[] indicies)
        {
            foreach (int index in indicies)
            {
                _enterEvent[index]?.Invoke();
            }
        }

        public void ExitEvents(int[] indicies)
        {
            foreach (int index in indicies)
            {
                _exitEvent[index]?.Invoke();
            }
        }
    }
}