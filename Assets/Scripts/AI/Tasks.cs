using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class Tasks : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;

    public float followDistance;
    public float attackDistance;
    public float flockDistance;

    private Vector3 currentLocation;
    private Vector3 playerLocation;

    private Movement movementScript;
    private MovementDetector movementDetector;

    private GameObject[] flock;

    private Vector3 movementDirection;

    Vector3 closestFlockerPos;
    float closestFlockerDistance;

    [Task]
    void IsPlayerNear()
    {
        if (Task.current.isStarting)
        {
            // Set 'player' equal to the transform of the GameObject with the 'Player' tag (should only be one object)
            player = GameObject.FindWithTag("Player").transform;

            // Set 'body' equal to the current gameObject's Rigidbody (for physics)
            rb = gameObject.GetComponent<Rigidbody>();
        }

        if (player == null)
        {
            Task.current.Fail();
            return;
        }

        float playerDistance2d = Vector2.Distance(transform.position, player.position);

        // Within follow distance of player
        if (playerDistance2d < followDistance)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    void IsPlayerInAttackRange()
    {
        if (Task.current.isStarting)
        {
            // Set 'player' equal to the transform of the GameObject with the 'Player' tag (should only be one object)
            player = GameObject.FindWithTag("Player").transform;

            // Set 'body' equal to the current gameObject's Rigidbody (for physics)
            rb = gameObject.GetComponent<Rigidbody>();
        }

        if (player == null)
        {
            Task.current.Fail();
            return;
        }

        float playerDistance2d = Vector2.Distance(transform.position, player.position);

        // Within follow distance of player
        if (playerDistance2d < attackDistance)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    void AttackPlayer()
    {
        if (player == null)
        {
            Task.current.Fail();
            return;
        }

        transform.Find("WeaponHitBox").GetComponent<MeleeAttack>().Attack();
        Task.current.Succeed();
    }

    [Task]
    void SetMovementDirection_TowardsPlayer()
    {
        if (Task.current.isStarting)
        {
            // Set 'player' equal to the transform of the GameObject with the 'Player' tag (should only be one object)
            player = GameObject.FindWithTag("Player").transform;

            // Set 'body' equal to the current gameObject's Rigidbody (for physics)
            rb = gameObject.GetComponent<Rigidbody>();
        }

        if (player == null)
        {
            Task.current.Fail();
            return;
        }

        // This might be slow
        flock = GameObject.FindGameObjectsWithTag("Enemy");

        // Iterate through all enemies and pick the closest one to us
        closestFlockerPos = Vector3.zero;
        closestFlockerDistance = flockDistance;
        foreach (var f in flock)
        {
            if (f == null || f == gameObject)
                continue;

            float curDistance = Vector2.Distance(transform.position, f.transform.position);
            if (curDistance < flockDistance)
            {
                closestFlockerPos = f.transform.position;
                closestFlockerDistance = curDistance;
            }
        }

        // find difference between our position and player's position
        Vector3 direction = player.position - transform.position;

        // We are too close to another enemy
        if (closestFlockerDistance < flockDistance)
        {
            //Debug.Log("Closest enemy is " + closestFlockerDistance + "m away - too close!");
            Vector3 avoidDir = transform.position - closestFlockerPos;
            avoidDir = avoidDir.normalized * (1 - closestFlockerDistance / flockDistance);
            direction += avoidDir;
        }

        // change to unit direction vector
        direction.Normalize();

        movementDirection = direction;
        Task.current.Succeed();
    }

    [Task]
    void MoveInDirection()
    {
        if (Task.current.isStarting)
        {
            // Set 'player' equal to the transform of the GameObject with the 'Player' tag (should only be one object)
            player = GameObject.FindWithTag("Player").transform;

            // Set 'body' equal to the current gameObject's Rigidbody (for physics)
            rb = gameObject.GetComponent<Rigidbody>();

            // sets value to the movement script
            movementScript = gameObject.GetComponent<Movement>();
        }

        // Move in specified direction
        movementScript.move(movementDirection.x, 0);
        Task.current.Succeed();
    }

    [Task]
    void Idle()
    {
        if (Task.current.isStarting)
        {
            // sets value to the movement script
            movementScript = gameObject.GetComponent<Movement>();
        }

        // Just don't move
        movementScript.move(0, 0);
        Task.current.Succeed();
    }

    [Task]
    void IsNearOtherEnemy()
    {
        // This might be slow
        flock = GameObject.FindGameObjectsWithTag("Enemy");

        // Iterate through all enemies and pick the closest one to us
        closestFlockerPos = Vector3.zero;
        closestFlockerDistance = flockDistance;
        foreach (var f in flock)
        {
            if (f == null || f == gameObject)
                continue;

            float curDistance = Vector2.Distance(transform.position, f.transform.position);
            if (curDistance < flockDistance)
            {
                closestFlockerPos = f.transform.position;
                closestFlockerDistance = curDistance;
            }
        }

        // We are too close to another enemy
        if (closestFlockerDistance < flockDistance)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    void SetDirection_Away()
    {
        // find difference between our position and player's position
        Vector3 direction = Vector3.zero;

        //Debug.Log("Closest enemy is " + closestFlockerDistance + "m away - too close!");
        Vector3 avoidDir = transform.position - closestFlockerPos;
        avoidDir = avoidDir.normalized * (1 - closestFlockerDistance / flockDistance);
        direction += avoidDir;

        // change to unit direction vector
        direction.Normalize();

        movementDirection = direction;
        Task.current.Succeed();
    }
}
