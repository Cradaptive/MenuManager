using DG.Tweening;
using System;
using UnityEngine;


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
}

public class ScreenCloser : UIAffector
{

    [ContextMenu("TestCloseMenu")]
    public override void Run()
    {
        Close(null);
    }

    public void Close(Action onClose = null)
    {
        if (transition != null)
        {
            transition.StartTransition(reverseTransition: true);
        }
        else
        {
            gameObject.SetActive(false);
            onClose?.Invoke();
        }
    }
}

