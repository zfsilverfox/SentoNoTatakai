using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private static string HorizontalMovingInputString = "Horizontal";
    private static string JumpKeyInputString = "Jump";

   
    public float HorizontalSpd;
    public bool JumpKeyPressed;
    public bool WeakAttack;
    public bool DashKeyPressed;
    public bool StrongAttack;







    BtnManagerClass JumpClass = new BtnManagerClass();
    BtnManagerClass AttackClass = new BtnManagerClass();
    BtnManagerClass DashClass = new BtnManagerClass();
    BtnManagerClass StrongAttackClass = new BtnManagerClass();




    private void Update()
    {

        HorizontalSpd = Input.GetAxis(HorizontalMovingInputString);
        JumpClass.PressedManager(Input.GetKeyUp(KeyCode.UpArrow)||Input.GetKeyUp(KeyCode.W));

        JumpKeyPressed = JumpClass.EndPressed;
        WeakAttack = AttackClass.EndPressed;
        AttackClass.PressedManager(Input.GetKeyDown(KeyCode.Space));


        DashClass.PressedManager(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S));
        DashKeyPressed = DashClass.EndPressed;


        StrongAttackClass.PressedManager(Input.GetKeyUp(KeyCode.Alpha1));
        StrongAttack = StrongAttackClass.StartPressed;









    }





}



public class BtnManagerClass
{
    public bool StartPressed;
    public bool isPressed;
    public bool EndPressed;

    public void PressedManager(bool Press)
    {
        StartPressed = Press;

        
        if(StartPressed != EndPressed)
        {
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }
        EndPressed = StartPressed;

    }





}
