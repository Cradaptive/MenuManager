using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradaptive.TransitionsTypes;
using UnityEngine.Events;
//[CreateAssetMenu(fileName = "ComplexTransitionData", menuName = "Transitions/Create Complex Transition Data", order = 2)]
[System.Serializable]
public class ComplexTransitionData
{
    [HideInInspector]
    public UnityEvent OnOpeningTransitionCompleted;
    [HideInInspector]
    public UnityEvent OnClosingTransitionCompleted;
    [HideInInspector]
    public TransitionHelperType entryTransitionHelper;
    public TransitionHelperType exitTransitionHelper;
}