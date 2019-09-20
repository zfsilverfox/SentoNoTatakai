using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEnterState : StateMachineBehaviour
{

    public string UpwardString;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(UpwardString != null)
        {
            animator.gameObject.SendMessageUpwards(UpwardString);
        }
    }
}
