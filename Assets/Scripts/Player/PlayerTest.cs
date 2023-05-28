using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : PlayerBase
{
    protected override void HandleQAbility()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !swordTestController.inAnimation)
        {
            swordTestController.HandleSwordSwingAnim("Swing");
        }
    }

    protected override void HandleWAbility()
    {
        if (Input.GetKeyDown(KeyCode.W) && !swordTestController.inAnimation)
        {
            swordTestController.HandleSwordSwingAnim("Spin");
        }
    }

    protected override void HandleEAbility()
    {
        if (Input.GetKeyDown(KeyCode.E) && !swordTestController.inAnimation)
        {
            state = State.idle;
            Vector3 posToDash = GetMousePosition();
            Vector3 direction = (posToDash - this.transform.position).normalized;
            HandleRotation(posToDash);
            rb.AddForce(direction * 25, ForceMode2D.Impulse);
            swordTestController.HandleSwordSwingAnim("Dash");
        }
    }

    protected override void HandleRAbility()
    {
        if (Input.GetKeyDown(KeyCode.R) && !swordTestController.inAnimation)
        {
            
        }
    }
}
