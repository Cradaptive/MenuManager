using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;

public class ScreenOpener : UIAffector
{
    [ContextMenu("TestOpenMenu")]
    public override void Run()
    {
        Open();
    }
    public void Open(Action onOpen = null)
    {
        if(transition!=null)
        {
            transition.StartTransition();
        }
        else
        {
            gameObject.SetActive(true);
            onOpen?.Invoke();
        }
    }
}
