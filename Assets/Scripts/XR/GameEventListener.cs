using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityUtils
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField]
        private GameEvent Event;
        [SerializeField]
        private UnityEvent Response;

        private void OnEnable()
        {
            this.Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            this.Event.UnRegisterListener(this);
        }

        public void OnRaiseEvent()
        {
            this.Response.Invoke();
        }
    }
}
