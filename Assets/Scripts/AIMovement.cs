using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AIMove : MonoBehaviour
{
    public Transform target;           // Player to follow
    public float roamRadius = 10f;     // Random roaming radius
    public float roamDelay = 3f;       // Delay between random destinations
    public Key keyToFollow = Key.X;    // Key to start following
    public float followDuration = 5f;  // How long the AI follows after key press

    private NavMeshAgent agent;
    private bool isFollowing = false;
    private float roamTimer = 0f;
    private float followTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        roamTimer = roamDelay; // pick a random point immediately
    }

    void Update()
    {
        // Check key press using the new Input System
        if (Keyboard.current[keyToFollow].wasPressedThisFrame)
        {
            isFollowing = true;
            followTimer = 0f; // reset follow timer
        }

        if (isFollowing)
        {
            followTimer += Time.deltaTime;

            var prevSpeed = agent.speed;
            if (target != null)
            {
                agent.SetDestination(target.position);
                agent.speed = 6f; // faster speed when following
            }

            // Stop following after the followDuration
            if (followTimer >= followDuration)
            {
                agent.speed = prevSpeed; // reset speed
                isFollowing = false;
            }
        }
        else
        {
            // Random roaming
            roamTimer += Time.deltaTime;
            if (!agent.pathPending && agent.remainingDistance < 0.5f || roamTimer >= roamDelay)
            {
                Vector3 newPos = RandomNavSphere(transform.position, roamRadius, -1);
                agent.SetDestination(newPos);
                roamTimer = 0f;
            }
        }
    }

    // Get a random point on the NavMesh within a radius
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist + origin;

        if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layerMask))
        {
            return navHit.position;
        }

        return origin; // fallback
    }
}
