using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIAffector : MonoBehaviour
{
    protected Transition transition;

    public void SetUpTranstion(Transition transition)
    {
        this.transition = transition;
    }

    public virtual void Run()
    {

    }

    public virtual void Run(UnityAction onComplete)
    {

    }
}

public class ScreenCloser : UIAffector
{

    [ContextMenu("TestCloseMenu")]
    public override void Run()
    {
        Run(null);
    }

    public override void Run(UnityAction onComplete = null)
    {
        if (transition != null)
        {
            transition.StartTransition(onComplete, reverseTransition: true);
        }
        else
        {
            Debug.LogError("There was no transition, Closing Screen without transition");
            gameObject.SetActive(false);
            onComplete?.Invoke();
        }
    }
}

