using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ScaleDirection { AllDirection, TopDown, BottomUp, LeftRight, RightLeft }

public class ScaleTransition : Transition
{
    ScaleDirection currentScaleDirection;
    public float duration = .5f, delay = 0;
    [ContextMenu("Test Transition")]
    public override void TestTransition()
    {
        Debug.LogError("testing scale");
        StartTransition();
    }

    public override void ReAdjustElements()
    {
        gameObject.SetActive(true);
        transform.localPosition = prevPosition;
        if (TryGetComponent(out CanvasGroup cg))
        {
            cg.alpha = 1;
        }
    }

    public override void MainTranslation(UnityAction onCompleteTransition = null,  bool reverseTransition = false)
    {
        Vector3 localMagnitude = reverseTransition ? Vector3.zero : Vector3.one;
        transform.localScale = !reverseTransition ? Vector3.zero : Vector3.one;

        bool state = !reverseTransition;

        prevTween = transform.DOScale(localMagnitude, duration).OnComplete(() =>
        {
            transitionData?.OnClosingTransitionCompleted?.Invoke();
            onCompleteTransition?.Invoke();
        }).SetDelay(delay);

    }

}
