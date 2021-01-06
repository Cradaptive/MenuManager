using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BounceTransitionHelper : TransitionHelper
{
    public float magnitude = 15, duration = .09f, delay = 0;

    [ContextMenu("Test Bounce")]
    public void TestTransition()
    {
        StartTransition();
    }

    public override void StartTransition(Action onCompleteTransition = null, bool reverseTransition = false)
    {
        Vector3 finalPosition = transform.position;
        transform.DOMoveY(finalPosition.y + magnitude, duration).OnComplete(() =>
        {
            transform.DOMoveY(finalPosition.y - magnitude, duration).OnComplete(() =>
            {
                transform.DOMoveY(finalPosition.y, duration).OnComplete(() =>
                {
                    onCompleteTransition?.Invoke();
                });
            });
        }).SetDelay(delay);
    }
}
