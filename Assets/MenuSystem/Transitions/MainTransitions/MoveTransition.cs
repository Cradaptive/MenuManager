using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cradaptive.TransitionsTypes;
using UnityEngine.Events;

public enum MoveDirection { Top, Bottom, Left, Right }

public class MoveTransition : Transition
{
    float upMagnitude = 1500;
    MoveDirection currentEntryDirection;
    float magnitude = 1000, duration = 1f, delay = 0;
    MoveTransitionData moveTransitionData;
    public Vector3 currentPosition;

    private void Awake()
    {
        currentPosition = transform.localPosition;
    }
    public override void SetUpData(ComplexTransitionData complexTransitionData)
    {
        base.SetUpData(complexTransitionData);
        moveTransitionData = complexTransitionData as MoveTransitionData;
    }

    public override void ReAdjustElements()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.one;
        if (TryGetComponent(out CanvasGroup cg))
        {
            cg.alpha = 1;
        }
    }

    public override void StartTransition(UnityAction onCompleteTransition = null, bool reverseTransition = false)
    {
        base.StartTransition(onCompleteTransition, reverseTransition);

    }

    public override void MainTranslation(UnityAction onCompleteTransition = null, bool reverseTransition = false)
    {
        float finalX = 0;
        float finalY = 0;
        Vector3 startPosition = reverseTransition ? currentPosition : new Vector3(-magnitude, currentPosition.y, 0);

        currentEntryDirection = reverseTransition ? moveTransitionData.exitDirection : moveTransitionData.entryDirection;


        if (currentEntryDirection == MoveDirection.Left)
        {
            startPosition = reverseTransition ? currentPosition : new Vector3(-magnitude, currentPosition.y, currentPosition.z);
            finalX = reverseTransition ? -magnitude : currentPosition.x;
            finalY = currentPosition.y;
        }
        else if (currentEntryDirection == MoveDirection.Right)
        {
            startPosition = reverseTransition ? currentPosition : new Vector3(magnitude, currentPosition.y, currentPosition.z);
            finalX = reverseTransition ? magnitude : currentPosition.x;
            finalY = currentPosition.y;
        }
        else if (currentEntryDirection == MoveDirection.Bottom)
        {
            startPosition = reverseTransition ? currentPosition : new Vector3(currentPosition.x, -upMagnitude, currentPosition.z);
            finalY = reverseTransition ? -magnitude : currentPosition.y;
            finalX = currentPosition.x;
        }
        else if (currentEntryDirection == MoveDirection.Top)
        {
            startPosition = reverseTransition ? currentPosition : new Vector3(currentPosition.x, upMagnitude, currentPosition.z);
            finalY = reverseTransition ? magnitude : currentPosition.y;
            finalX = currentPosition.x;
        }

        transform.localPosition = startPosition;

        prevTween = transform.DOLocalMove(new Vector3(finalX, finalY, 0), duration).SetDelay(delay).OnComplete(() =>
        {
            transitionData?.OnClosingTransitionCompleted?.Invoke();
            onCompleteTransition?.Invoke();
        });
    }

}
