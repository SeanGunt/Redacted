using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static Utilities instance;

    private void Awake()
    {
        instance = this;
    }

    public void HandleRotation(Vector3 rotationTargetPosition, Transform thingToRotate)
    {
        Vector3 direction = (rotationTargetPosition - thingToRotate.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        thingToRotate.eulerAngles = new Vector3(0,0,angle);
    }
    
}
