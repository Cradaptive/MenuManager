using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradaptive.TransitionsTypes;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[RequireComponent(typeof(CanvasGroup))]
public class FadeTransition : Transition
{
    CanvasGroup canvasGroup;
    float magnitude = 1, duration = 1f, delay = 0;

    public override void ReAdjustElements()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.one;
    }

    public override void MainTranslation(UnityAction onCompleteTransition = null, bool reverseTransition = false)
    {
        canvasGroup = GetComponent<CanvasGroup>();

        float localMagnitude = reverseTransition ? 0f : magnitude;
        canvasGroup.alpha = canvasGroup!=null && !reverseTransition ? 0f : magnitude;
        bool state = !reverseTransition;
        if (canvasGroup != null)
        {
            prevTween = canvasGroup.DOFade(localMagnitude, duration).OnComplete(() =>
            {
                transitionData?.OnClosingTransitionCompleted?.Invoke();
                onCompleteTransition?.Invoke();
                canvasGroup.interactable = canvasGroup.blocksRaycasts = state;
                
            }).SetDelay(delay);
        }
        else
        {
            Debug.LogError("We cant fade if there is no canvas group attached to affector object");
        }
    }

}
