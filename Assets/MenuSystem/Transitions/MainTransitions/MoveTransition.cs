using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cradaptive.TransitionsTypes;

public enum MoveDirection { Top, Bottom, Left, Right }

public class MoveTransition : Transition
{
    float upMagnitude = 1500;
    MoveDirection currentEntryDirection;
    float magnitude = 1000, duration = 1f, delay = 0;
    MoveTransitionData moveTransitionData;
    Vector3 currentPosition;

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
    }

    public override void StartTransition(Action onCompleteTransition = null, bool reverseTransition = false)
    {
        base.StartTransition(onCompleteTransition, reverseTransition);

    }

    public override void MainTranslation(Action onCompleteTransition = null, bool reverseTransition = false)
    {
        float finalX = 0;
        float finalY = 0;
        Vector3 startPosition = reverseTransition ? Vector3.zero : new Vector3(-magnitude, 0, 0);

        currentEntryDirection = reverseTransition ? moveTransitionData.exitDirection : moveTransitionData.entryDirection;


        if (currentEntryDirection == MoveDirection.Left)
        {
            startPosition = reverseTransition ? currentPosition : new Vector3(-magnitude, currentPosition.y, 0);
            finalX = reverseTransition ? -magnitude : 0;
        }
        else if (currentEntryDirection == MoveDirection.Right)
        {
            startPosition = reverseTransition ? currentPosition : new Vector3(magnitude, currentPosition.y, 0);
            finalX = reverseTransition ? magnitude : 0;
        }
        else if (currentEntryDirection == MoveDirection.Bottom)
        {
            startPosition = reverseTransition ? currentPosition : new Vector3(currentPosition.z, -upMagnitude, 0);
            finalY = reverseTransition ? -magnitude : 0;
        }
        else if (currentEntryDirection == MoveDirection.Top)
        {
            startPosition = reverseTransition ? currentPosition : new Vector3(currentPosition.z, upMagnitude, 0);
            finalY = reverseTransition ? magnitude : 0;
        }

        transform.localPosition = startPosition;

        prevTween = transform.DOLocalMove(new Vector3(finalX, finalY, 0), duration).SetDelay(delay).OnComplete(() =>
        {
            transitionData?.OnClosingTransitionCompleted?.Invoke();
            onCompleteTransition?.Invoke();
        });
    }

}
