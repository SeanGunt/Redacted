using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAnimation : MonoBehaviour
{
    [HideInInspector] public bool jawOpened;
    public void JawOpened()
    {
        jawOpened = true;
    }

    public void JawClosed()
    {
        jawOpened = false;
    }
}
