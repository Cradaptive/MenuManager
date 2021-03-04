using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCompletedElementTransition : UnityEvent<int> { };
public class GroupTransitionElements : MonoBehaviour
{
    public bool findElementsInChildren;
    public bool controlWithHeirachy;
    public OnCompletedElementTransition startTransitionOfNextElement = new OnCompletedElementTransition();
    public OnCompletedElementTransition reverseTransitionOfNextElement = new OnCompletedElementTransition();
    public OnCompletedElementTransition onBroadCastElementCompletedTransition = new OnCompletedElementTransition();
    public TransitionElement[] transitionElements;
    bool reversing;


    private void OnEnable()
    {
        onBroadCastElementCompletedTransition.AddListener(ElementTransitionCompleted);
    }

    private void Awake()
    {
        if (findElementsInChildren)
            transitionElements = GetComponentsInChildren<TransitionElement>();

        for (int i = 0; i < transitionElements.Length; i++)
        {
            transitionElements[i].SetTransitionGroup(this);
        }


    }

    void ElementTransitionCompleted(int indexOfCompleted)
    {
        if (indexOfCompleted + 1 > transitionElements.Length)
        {
            reversing = false;
            return;
        }

        if (!reversing)
            startTransitionOfNextElement?.Invoke(indexOfCompleted + 1);
        else
            reverseTransitionOfNextElement?.Invoke(indexOfCompleted + 1);
    }

    [ContextMenu("Show All Elements")]
    public void ShowAllElements()
    {
        reversing = false;
        if (controlWithHeirachy)
            startTransitionOfNextElement?.Invoke(0);
        else
            for (int i = 0; i < transitionElements.Length; i++)
                transitionElements[i].Open();
    }

    [ContextMenu("Hide All Elements")]
    public void HideAllElements()
    {
        reversing = true;
        if (controlWithHeirachy)
            reverseTransitionOfNextElement?.Invoke(0);
        else
            for (int i = 0; i < transitionElements.Length; i++)
                transitionElements[i].Close();
    }

}
