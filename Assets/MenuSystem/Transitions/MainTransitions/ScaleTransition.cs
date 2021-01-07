using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTransition : Transition
{
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
    }

    public override void MainTranslation(Action onCompleteTransition = null,  bool reverseTransition = false)
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
