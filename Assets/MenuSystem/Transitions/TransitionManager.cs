using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Cradaptive.Transitions
{
   
}

public class TransitionManager : MonoBehaviour
{
    Transition[] transitions;

    private void Awake()
    {
        transitions = Resources.LoadAll("CradaptiveTransitions") as Transition[];
    }

    public void Transtion(string transitionName = "")
    {
        //if(!string.IsNullOrEmpty(transitionName))
        //{
        //    Transition transition = transitions.FirstOrDefault(x => x.transitionName.ToUpper() == transitionName.ToUpper());
        //    transition.StartTransition();
        //}
    }

}
