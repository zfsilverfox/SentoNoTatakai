using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorExitState : StateMachineBehaviour
{
    public string UpwardMessage;



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(UpwardMessage != null)
        {
            animator.gameObject.SendMessageUpwards(UpwardMessage);
        }


    }

    
}
