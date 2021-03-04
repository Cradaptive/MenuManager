using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradaptive.TransitionsTypes;
using DG.Tweening;
using UnityEngine.Events;

namespace Cradaptive.TransitionsTypes
{
    public enum TransitionType { None, Fade, Move, Scale }
    public enum TransitionHelperType { None,Shake, Bounce }
    public enum TransitionHelperOwner { Opener, Closer }
}

public abstract class Transition : MonoBehaviour
{
    protected Tween prevTween;
    protected TransitionHelper entryTransitionHelper;
    protected TransitionHelper exitTransitionHelper;
    protected ComplexTransitionData transitionData;
    protected Vector3 prevPosition;
    private void Awake()
    {
        prevPosition = transform.localPosition;
    }

    [ContextMenu("Test Transition")]
    public virtual void TestTransition()
    {

    }


    public TransitionHelper GetTransition(TransitionHelperType transitionHelperType)
    {
        TransitionHelper helper = null;
        switch (transitionHelperType)
        {
            case TransitionHelperType.Bounce:
                helper = gameObject.AddComponent<BounceTransitionHelper>();
                break;
            case TransitionHelperType.Shake:
                helper = gameObject.AddComponent<ShakeTransitionHelper>();
                break;
        }
        return helper;
    }

    public virtual void SetUpData(ComplexTransitionData complexTransitionData)
    {
        this.transitionData = complexTransitionData;
        entryTransitionHelper = GetTransition(transitionData.entryTransitionHelper);
        exitTransitionHelper = GetTransition(transitionData.exitTransitionHelper);
    }

    public virtual void ReAdjustElements()
    {

    }

    public virtual void StartTransition(UnityAction onCompleteTransition = null,  bool reverseTransition = false)
    {
        ReAdjustElements();

        prevTween?.Kill();

        if (entryTransitionHelper == null && exitTransitionHelper == null)
        {
            MainTranslation(onCompleteTransition, reverseTransition);
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
                            transitionData?.OnOpeningTransitionCompleted?.Invoke();
                            onCompleteTransition?.Invoke();
                           
                        });
                    }

                },reverseTransition);
            }
            else
            {
                if (exitTransitionHelper != null)
                {
                    exitTransitionHelper.StartTransition(() =>
                    {
                        MainTranslation(()=> { onCompleteTransition?.Invoke(); ReAdjustElements(); }, reverseTransition);
                    });
                }
            }
        }
    }

    public virtual void MainTranslation(UnityAction onCompleteTransition = null, bool reverseTransition = false)
    {

    }
}
