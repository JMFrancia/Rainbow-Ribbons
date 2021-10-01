using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils;

public class MuteSwitchToGameEvent : MonoBehaviour
{
    [SerializeField] private GameEvent muteOn;
    [SerializeField] private GameEvent muteOff;

    public void OnMuteSwitchChanged(bool muted)
    {
        if (muted) muteOn?.Raise();
        else muteOff?.Raise();
    }
}
