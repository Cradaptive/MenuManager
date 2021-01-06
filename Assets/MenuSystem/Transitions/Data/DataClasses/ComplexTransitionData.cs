using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradaptive.TransitionsTypes;

//[CreateAssetMenu(fileName = "ComplexTransitionData", menuName = "Transitions/Create Complex Transition Data", order = 2)]
[System.Serializable]
public class ComplexTransitionData
{
 //   public float magnitude = 1, duration = .5f, delay = 0;
    public TransitionHelperType entryTransitionHelper;
    public TransitionHelperType exitTransitionHelper;
}