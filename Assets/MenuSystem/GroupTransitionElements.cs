using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupTransitionElements : MonoBehaviour
{
    public TransitionElement[] transitionElements;
    public bool findElementsInChildren;

    private void Awake()
    {
        if(findElementsInChildren)
        transitionElements = GetComponentsInChildren<TransitionElement>();
    }

    [ContextMenu("Show All Elements")]
    public void ShowAllElements()
    {
        for (int i = 0; i < transitionElements.Length; i++)
            transitionElements[i].Open();
    }

    [ContextMenu("Hide All Elements")]
    public void HideAllElements()
    {
        for (int i = 0; i < transitionElements.Length; i++)
            transitionElements[i].Close();
    }

}
