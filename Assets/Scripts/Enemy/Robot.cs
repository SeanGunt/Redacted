using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : EnemyMaster
{
    [Header("Robot Specifics")]
    [SerializeField] private AudioClip laserStartClip;
    [SerializeField] private AudioClip laserFiringClip;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject laserSpawnPoint;
    private RobotLaser robotLaser;
    private float laserTimer = 6f;
    private float timeTillLaser;
    private State state;
    private enum State
    {
        moving, startLaser, lasering
    }

    private void OnEnable()
    {
        state = State.moving;
    }
    protected override void Update()
    {
        if (dead || frozen) return;
        switch (state)
        {
            case State.moving:
                timeTillLaser -= Time.deltaTime;
                if (DistanceToPlayer() <= 6f && state != State.lasering && timeTillLaser <= 0 && !frozen)
                {
                    state = State.startLaser;
                    animator.SetTrigger("Laser");
                }
                animator.SetBool("Moving", true);
                Rotation();
                Movement();
                break;
            case State.startLaser:
                StartCoroutine(Laser());
                break;
            case State.lasering:
                agent.isStopped = true;
                break;
        }

    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    private void OnDestroy()
    {
        if (robotLaser != null)
        {
            robotLaser.fadeStarted = true;
        }
    }

    private IEnumerator Laser()
    {
        ChangeSpeed(1.5f, SpeedChange.decrease);
        state = State.lasering;
        audioSource.PlayOneShot(laserStartClip);
        yield return new WaitForSeconds(0.6f);
        GameObject laser =  Instantiate(laserPrefab, laserSpawnPoint.transform.position, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
        audioSource.PlayOneShot(laserFiringClip);
        robotLaser = laser.GetComponentInChildren<RobotLaser>();
        laser.transform.localScale = new Vector3(0.25f, 0f, 1f);
        Utilities.instance.HandleRotation(player.transform.position, laser.transform);
        while (laser.transform.localScale.y <= 8f)
        {
            if (frozen || dead)
            {
                break;
            }
            laser.transform.localScale += new Vector3(0f, Time.deltaTime * 12.5f, 0f);
            yield return null;
        }
        timeTillLaser = laserTimer;
        robotLaser.fadeStarted = true;
        agent.isStopped = false;
        state = State.moving;
        animator.SetBool("Moving", false);
        yield return new WaitForSeconds(5f);
        ChangeSpeed(1.5f, SpeedChange.increase);
    }
}
