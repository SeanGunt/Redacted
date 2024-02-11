using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : EnemyMaster
{
    [Header("Robot Specifics")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject laserSpawnPoint;
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
        Rotation();

        switch (state)
        {
            case State.moving:
                timeTillLaser -= Time.deltaTime;
                if (DistanceToPlayer() <= 6f && state != State.lasering && timeTillLaser <= 0)
                {
                    state = State.startLaser;
                }
                Movement();
                break;
            case State.startLaser:
                StartCoroutine(Laser());
                break;
            case State.lasering:
                Lasering();
                break;
        }

    }
    protected override void Movement()
    {
        agent.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z));
    }

    protected override void Rotation()
    {
        HandleRotation(player.transform.position, transform);
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    private void Lasering()
    {
        agent.isStopped = true;
    }

    private IEnumerator Laser()
    {
        state = State.lasering;
        yield return new WaitForSeconds(1.5f);
        GameObject laser =  Instantiate(laserPrefab, laserSpawnPoint.transform.position, Quaternion.identity, GameManager.Instance.poolHolders[3].transform);
        RobotLaser robotLaser = laser.GetComponentInChildren<RobotLaser>();
        HandleRotation(player.transform.position, laser.transform);
        laser.transform.localScale = new Vector3(0.25f, 0f, 1f);
        while (laser.transform.localScale.y <= 8f)
        {
            laser.transform.localScale += new Vector3(0f, Time.deltaTime * 10f, 0f);
            yield return null;
        }
        timeTillLaser = laserTimer;
        robotLaser.fadeStarted = true;
        agent.isStopped = false;
        state = State.moving;
    }
}
