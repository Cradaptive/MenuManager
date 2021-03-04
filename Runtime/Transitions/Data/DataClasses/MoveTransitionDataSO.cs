using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MoveTransitionData", menuName = "Transitions/Create Move Transition Data", order = 3)]
[System.Serializable]
public class MoveTransitionDataSO : ComplexTransitionDataSO
{
    public float upMagnitude = 1500;
    public MoveDirection entryDirection, exitDirection;
}
