using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine.Events;

public class ScreenOpener : UIAffector
{
    [ContextMenu("TestOpenMenu")]
    public override void Run()
    {
        Run();
    }
    public override void Run(UnityAction onComplete = null)
    {
        if(transition!=null)
        {
            transition.StartTransition(onComplete);
        }
        else
        {
            Debug.LogError("There was no transition, Closing Screen without transition");
            gameObject.SetActive(true);
            onComplete?.Invoke();
        }
    }
}
