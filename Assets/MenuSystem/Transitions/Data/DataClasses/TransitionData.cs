using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradaptive.TransitionsTypes;
using UnityEngine.Events;

//[CreateAssetMenu(fileName = "TransitionData", menuName = "Transitions/Create Transition Data", order = 1)]
[System.Serializable]
public class TransitionData
{
    [HideInInspector]
    public UnityEvent OnOpeningTransitionCompleted;
    [HideInInspector]
    public UnityEvent OnClosingTransitionCompleted;
    [HideInInspector]
    public TransitionHelperOwner transitionHelperOwner;
    public float magnitude = 1, duration = .5f, delay = 0;
}
