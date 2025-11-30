using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AIMove : MonoBehaviour
{
    public Transform target;
    public float roamRadius = 10f;
    public float roamDelay = 3f;
    public Key keyToFollow = Key.X;
    public float followDuration = 5f;
    public float sightRange = 10f;
    public float followSpeed = 6f;

    private NavMeshAgent agent;
    private bool isFollowing = false;
    private float roamTimer = 0f;
    private float followTimer = 0f;
    private float originalSpeed;

    // NEW — lingering follow after losing sight
    public float lostSightDuration = 1f;
    private float lostSightTimer = 0f;
    private bool sawPlayerLastFrame = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalSpeed = agent.speed;
        roamTimer = roamDelay;
    }

    void Update()
    {
        bool seesPlayer = false;

        // Vision check
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;

            if (direction.magnitude <= sightRange)
            {
                if (!Physics.Raycast(transform.position + Vector3.up, direction.normalized, out RaycastHit hit, sightRange)
                    || hit.transform == target)
                {
                    seesPlayer = true;
                }
            }
        }

        // -------------------------------
        // NEW: Handle lingering follow
        // -------------------------------
        if (seesPlayer)
        {
            lostSightTimer = 0f;          // reset timer when player seen
            sawPlayerLastFrame = true;
        }
        else if (sawPlayerLastFrame)
        {
            lostSightTimer += Time.deltaTime;

            if (lostSightTimer < lostSightDuration)
            {
                seesPlayer = true;       // artificially continue "seeing" the player
            }
            else
            {
                sawPlayerLastFrame = false;
            }
        }

        // Key press follow
        if (Keyboard.current[keyToFollow].wasPressedThisFrame)
        {
            isFollowing = true;
            followTimer = 0f;
        }

        // Follow if key-following OR sees player (including linger follow)
        if (isFollowing || seesPlayer)
        {
            followTimer += Time.deltaTime;

            if (target != null)
            {
                agent.SetDestination(target.position);
                agent.speed = followSpeed;
            }

            if (isFollowing && followTimer >= followDuration)
            {
                agent.speed = originalSpeed;
                isFollowing = false;
            }
        }
        else
        {
            // Roaming
            roamTimer += Time.deltaTime;
            if ((!agent.pathPending && agent.remainingDistance < 0.5f) || roamTimer >= roamDelay)
            {
                Vector3 newPos = RandomNavSphere(transform.position, roamRadius, -1);
                agent.SetDestination(newPos);
                roamTimer = 0f;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist + origin;

        if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layerMask))
        {
            return navHit.position;
        }

        return origin;
    }
}
