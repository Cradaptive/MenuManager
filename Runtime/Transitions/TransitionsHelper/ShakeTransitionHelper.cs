using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public abstract class TransitionHelper : MonoBehaviour
{
    public virtual void SetUpData(TransitionData transitionData)
    {
     
    }

    public virtual void StartTransition(Action onCompleteTransition = null, bool reverseTransition = false)
    {

    }
}

public class ShakeTransitionHelper : TransitionHelper
{
    float magnitude = 15, duration = .04f, delay = 0;

    public override void StartTransition(Action onCompleteTransition = null, bool reverseTransition = false)
    {
        Vector3 finalPosition = transform.position;

        transform.DOMoveX(finalPosition.x + magnitude, duration).OnComplete(() =>
        {
            transform.DOMoveY(finalPosition.y + magnitude, duration).OnComplete(() =>
            {
                transform.DOMoveX(finalPosition.x - magnitude, duration).OnComplete(() =>
                {
                    transform.DOMoveY(finalPosition.y - magnitude, duration).OnComplete(() =>
                    {
                        transform.DOMoveX(finalPosition.x, duration).OnComplete(() =>
                        {
                            transform.DOMoveY(finalPosition.y, duration).OnComplete(() =>
                            {
                                transform.DOMoveY(finalPosition.y, duration).OnComplete(() =>
                                {
                                    onCompleteTransition?.Invoke();
                                });
                            });
                        });
                    });
                });
            });
        }).SetDelay(delay);
    }
}
