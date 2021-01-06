using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradaptive.TransitionsTypes;

[RequireComponent(typeof(CanvasGroup))]
public class FadeTransition : Transition
{
    CanvasGroup canvasGroup;
    Tween prevTween;
    float magnitude = 1, duration = .5f, delay = 0;

    public override void StartTransition(Action onCompleteTransition = null, float delay = 0, bool reverseTransition = false)
    {
        base.StartTransition(onCompleteTransition, delay, reverseTransition);

        prevTween?.Kill();

        if (entryTransitionHelper == null && exitTransitionHelper == null)
        {
            MainTranslation(onCompleteTransition, delay, reverseTransition);
        }
        else
        {
            if (!reverseTransition)
            {
                MainTranslation(() =>
                {
                    if (entryTransitionHelper != null)
                    {
                        entryTransitionHelper.StartTransition(() =>
                        {
                            onCompleteTransition?.Invoke();
                        });
                    }

                }, delay, reverseTransition);
            }
            else
            {
                if (exitTransitionHelper != null)
                {
                    exitTransitionHelper.StartTransition(() =>
                    {
                        MainTranslation(onCompleteTransition, delay, reverseTransition);
                    });
                }
            }
        }
    }

    public void MainTranslation(Action onCompleteTransition = null, float delay = 0, bool reverseTransition = false)
    {
        canvasGroup = GetComponent<CanvasGroup>();

        transform.localPosition = Vector3.zero;

        if (reverseTransition && canvasGroup != null)
        {
            canvasGroup.alpha = 0;
        }

        float localMagnitude = reverseTransition ? 0 : magnitude;
        bool state = !reverseTransition;
        if (canvasGroup != null)
        {
            prevTween = canvasGroup.DOFade(localMagnitude, duration).OnComplete(() =>
            {
                onCompleteTransition?.Invoke();
                canvasGroup.interactable = canvasGroup.blocksRaycasts = state;
                gameObject?.SetActive(state);
            }).SetDelay(delay);
        }
        else
        {
            Debug.LogError("We cant fade if there is no canvas group attached to affector object");
        }
    }

}
