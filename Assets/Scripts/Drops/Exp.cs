using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : DropsBase
{
    [SerializeField] private float expAmount;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            HandleExperienceGain();
            gameObject.SetActive(false);
        }
    }
    /* Start at level 1. Each level will require a certain amount of experience to reach the next level. Games will last 20 minutes so ideally the player should reach level 18
    at 15 mins when they achieved max meta progression and 18 mins when they have no meta progression. Lets assume it takes 100 xp to reach level 2. Level 3 should be around 250 xp
    and level four should be 750 and so on and so on.... We will need a couple vars.

    float exp: this should be the total amount of experience the player has gained
    float expTillNextLevel: This should be the variable that goes up ONLY once the player has gained a level. It should be relative to the players current level
        and will determine how much xp they need to reach the next level*/
    private void HandleExperienceGain()
    {
        playerBase.exp += expAmount;
    }
}
