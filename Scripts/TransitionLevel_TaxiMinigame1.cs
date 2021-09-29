using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLevel_TaxiMinigame1 : MonoBehaviour
{
    public Animator transition;

    public void LoadTransitionStart()
    {
        transition.SetBool("Start", true);
    }
    public void LoadTransitionEnd()
    {
        transition.SetBool("Start", false);
    }


}
