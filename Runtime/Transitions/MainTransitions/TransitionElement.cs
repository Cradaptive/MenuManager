using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradaptive.TransitionsTypes;
using UnityEngine.Events;
using System.Linq;
using System;

[RequireComponent(typeof(ScreenOpener), typeof(ScreenCloser))]
public class TransitionElement : MonoBehaviour
{
    public int heirachy;
    ScreenCloser screenCloser;
    ScreenOpener screenOpener;
    [HideInInspector, Header("Opening Settings")]
    public TransitionType openingTransitionType;
    [HideInInspector]
    public TransitionHelperType postOpeningTransitionEffect;
    [HideInInspector]
    public MoveDirection entryDirection;
    MoveTransitionData OpeningTransitionData = new MoveTransitionData();
    public UnityEvent OnOpeningTransitionCompleted;

    [HideInInspector, Header("Closing Settings")]
    public TransitionType closingTransitionType;
    [HideInInspector]
    public TransitionHelperType preClosingTransitionEffect;
    [HideInInspector]
    public MoveDirection exitDirection;
    MoveTransitionData ClosingTransitionData = new MoveTransitionData();
    public UnityEvent OnClosingTransitionCompleted;
    GroupTransitionElements transitionGroup;
    public bool alreadyAdded;

    private void Awake()
    {
        SetUpTransitions();
    }

    public void SetTransitionGroup(GroupTransitionElements transitionGroup)
    {
        this.transitionGroup = transitionGroup;
        transitionGroup.startTransitionOfNextElement.AddListener(Run);
        transitionGroup.reverseTransitionOfNextElement.AddListener(Reverse);
        heirachy = Array.IndexOf(transitionGroup.transitionElements, this);
    }

    public void Run(int heirachy)
    {
        if (this.heirachy == heirachy)
        {
            StartTransition(() =>
            {
                transitionGroup?.onBroadCastElementCompletedTransition?.Invoke(heirachy);
            });
        }
    }

    public void Reverse(int heirachy)
    {
        if (this.heirachy == heirachy)
        {
            ReverseTransition(() =>
            {
                transitionGroup?.onBroadCastElementCompletedTransition?.Invoke(heirachy);
            });
        }
    }

    public void SetUpTransitions()
    {
        screenCloser = GetComponent<ScreenCloser>();
        screenOpener = GetComponent<ScreenOpener>();
        Transition[] transitions = GetComponents<Transition>();
        TransitionHelper[] transitionHelper = GetComponents<TransitionHelper>();

        for (int i = 0; i < transitions.Length; i++)
        {
            DestroyImmediate(transitions[i]);
        }
        for (int i = 0; i < transitionHelper.Length; i++)
        {
            DestroyImmediate(transitionHelper[i]);
        }
        if (TryGetComponent(out CanvasGroup canvasGroup))
        {
            DestroyImmediate(canvasGroup);
        }
        Transition transition = GetTransition(closingTransitionType);
        if (transition != null)
        {
            ClosingTransitionData = new MoveTransitionData();
            ClosingTransitionData.exitDirection = exitDirection;
            ClosingTransitionData.exitTransitionHelper = preClosingTransitionEffect;
            ClosingTransitionData.OnClosingTransitionCompleted = OnClosingTransitionCompleted;
            transition.SetUpData(ClosingTransitionData);
        }
        screenCloser.SetUpTranstion(transition);
        transition = GetTransition(openingTransitionType);
        if (transition != null)
        {
            OpeningTransitionData = new MoveTransitionData();
            OpeningTransitionData.entryDirection = entryDirection;
            OpeningTransitionData.entryTransitionHelper = postOpeningTransitionEffect;
            OpeningTransitionData.OnOpeningTransitionCompleted = OnOpeningTransitionCompleted;
            transition.SetUpData(OpeningTransitionData);
        }
        screenOpener.SetUpTranstion(transition);
    }

    Transition GetTransition(TransitionType transitionHelperType)
    {
        Transition transition = null;
        switch (transitionHelperType)
        {
            case TransitionType.Fade:
                transition = gameObject.AddComponent<FadeTransition>();
                break;
            case TransitionType.Move:
                transition = gameObject.AddComponent<MoveTransition>();
                break;
            case TransitionType.Scale:
                transition = gameObject.AddComponent<ScaleTransition>();
                break;
        }
        return transition;
    }
    [ContextMenu("Open Menu")]
    public void Open()
    {
        screenOpener.Run();
    }

    public void StartTransition(UnityAction onCompleteTransition)
    {
        screenOpener.Run(onCompleteTransition);
    }

    public void ReverseTransition(UnityAction onCompleteTransition)
    {
        screenCloser.Run(onCompleteTransition);
    }

    [ContextMenu("Close Menu")]
    public void Close()
    {
        screenCloser.Run();
    }

}
