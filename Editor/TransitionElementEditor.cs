using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cradaptive.TransitionsTypes;

[CustomEditor(typeof(TransitionElement))]
public class TransitionElementEditor : Editor
{
    ComplexTransitionDataSO fadeTransitionData;
    MoveTransitionDataSO moveTransitionData;
    TransitionDataSO bounceData;
    TransitionDataSO shakeData;

    public override void OnInspectorGUI()
    {
        var transitionElement = target as TransitionElement;
        if (GUILayout.Button("Setup"))
        {
            transitionElement.SetUpTransitions();
        }

        EditorGUILayout.LabelField("Opening Transtion Settings", EditorStyles.boldLabel);
        transitionElement.openingTransitionType = (TransitionType)EditorGUILayout.EnumPopup("Opening Transtion Type:", transitionElement.openingTransitionType);
        switch (transitionElement.openingTransitionType)
        {
            case TransitionType.Move:
                transitionElement.entryDirection = (MoveDirection)EditorGUILayout.EnumPopup("Entry Direction:", transitionElement.entryDirection);
                break;
            case TransitionType.Fade:
                break;
            default:
                break;
        }
        if (transitionElement.openingTransitionType != TransitionType.None)
        {
            transitionElement.postOpeningTransitionEffect = (TransitionHelperType)EditorGUILayout.EnumPopup("After Opening Effect:", transitionElement.postOpeningTransitionEffect);
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Closing Transtion Settings", EditorStyles.boldLabel);

        transitionElement.closingTransitionType = (TransitionType)EditorGUILayout.EnumPopup("Closing Transtion Type:", transitionElement.closingTransitionType);

        switch (transitionElement.closingTransitionType)
        {
            case TransitionType.Move:
                transitionElement.exitDirection = (MoveDirection)EditorGUILayout.EnumPopup("Exit Direction:", transitionElement.exitDirection);
                break;
            case TransitionType.Fade:
                break;
            default:
                break;
        }
        if (transitionElement.closingTransitionType != TransitionType.None)
        {
            transitionElement.preClosingTransitionEffect = (TransitionHelperType)EditorGUILayout.EnumPopup("Before Closing Effect:", transitionElement.preClosingTransitionEffect);
        }

        base.OnInspectorGUI();
    }

    public void ResetValues()
    {
        fadeTransitionData = fadeTransitionData == null ? (ComplexTransitionDataSO)EditorGUIUtility.Load("CradaptiveTransitionData/FadeTransitionData.asset") : fadeTransitionData;
        moveTransitionData = moveTransitionData == null ? (MoveTransitionDataSO)EditorGUIUtility.Load("CradaptiveTransitionData/MoveTransitionData.asset") : moveTransitionData;
        bounceData = bounceData == null ? (TransitionDataSO)EditorGUIUtility.Load("CradaptiveTransitionData/BounceTransitionData.asset") : bounceData;
        shakeData = shakeData == null ? (TransitionDataSO)EditorGUIUtility.Load("CradaptiveTransitionData/ShakeTransitionData.asset") : shakeData;

        var transitionElement = target as TransitionElement;

    }
}