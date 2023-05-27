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
}
