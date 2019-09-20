using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUpdateState : StateMachineBehaviour
{
    public string UpwardMessage;



    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(UpwardMessage != null)
        {
            animator.gameObject.SendMessageUpwards(UpwardMessage);
        }


    }

  
}
