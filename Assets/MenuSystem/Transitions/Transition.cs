using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradaptive.TransitionsTypes;

namespace Cradaptive.TransitionsTypes
{
    public enum TransitionType { None, Fade, Move }
    public enum TransitionHelperType { None,Shake, Bounce }
    public enum TransitionHelperOwner { Opener, Closer }
}

public abstract class Transition : MonoBehaviour
{
    protected TransitionHelper entryTransitionHelper;
    protected TransitionHelper exitTransitionHelper;
    protected ComplexTransitionData transitionData;

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

    public virtual void StartTransition(Action onCompleteTransition = null,  bool reverseTransition = false)
    {
        gameObject.SetActive(true);
    }
}
