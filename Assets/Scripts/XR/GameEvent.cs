using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils
{
    [CreateAssetMenu(menuName = "Events/Game Event")]
    public class GameEvent : ScriptableObject
    {

        private object m_lock = new object();

        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void RegisterListener(GameEventListener listener)
        {
            lock(m_lock)
            {
                if (!this.listeners.Contains(listener))
                {
                    //Debug.Log("Adding " + listener.name);
                    this.listeners.Add(listener);
                }
            }
        }

        public void UnRegisterListener(GameEventListener listener)
        {
            lock(m_lock)
            {
                if (this.listeners.Contains(listener))
                {
                    //Debug.Log("Removing " + listener.name);
                    this.listeners.Remove(listener);
                }
            }
        }

        public void Raise()
        {
            Debug.Log("<color=green>" + this.name + "</color> event launched");

            List<GameEventListener> tempListeners;

            lock(m_lock)
            {
                tempListeners = new List<GameEventListener>(this.listeners);
            }

            foreach (GameEventListener listener in tempListeners)
            {
                listener?.OnRaiseEvent();
                Debug.Log("<color=blue>" + listener.name + "</color> listener reacted");
            }
        }
    }
}