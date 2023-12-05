using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDamage : MonoBehaviour
{
    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject staff = GameObject.FindWithTag("Staff");
   
        if (staff) 
        {
            Staff staffMethods = staff.GetComponent<Staff>();
            if (staffMethods) 
            {
                IDamagable damagable = col.gameObject.GetComponent<IDamagable>();

                if (damagable != null)
                {
                    damagable.TakeDamage(staffMethods.ApplyDamage());
                }
            } else 
            {
                Debug.Log("staffMethods not accessible");
            }
        } else 
        {
            Debug.Log("staff not initialized");
        }   
    }
}
