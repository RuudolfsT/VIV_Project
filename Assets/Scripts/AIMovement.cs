using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AIMove : MonoBehaviour
{
    public Transform target;             // Player to follow
    public float roamRadius = 10f;       // Random roaming radius
    public float roamDelay = 3f;         // Delay between random destinations
    public Key keyToFollow = Key.X;      // Key to start following
    public float followDuration = 5f;    // How long the AI follows after key press
    public float sightRange = 10f;       // Distance at which AI can "see" the player
    public float followSpeed = 6f;       // Speed when following player

    private NavMeshAgent agent;
    private bool isFollowing = false;
    private float roamTimer = 0f;
    private float followTimer = 0f;
    private float originalSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalSpeed = agent.speed;
        roamTimer = roamDelay; // pick a random point immediately
    }

    void Update()
    {
        bool seesPlayer = target != null && Vector3.Distance(transform.position, target.position) <= sightRange;

        // Key press to start following
        if (Keyboard.current[keyToFollow].wasPressedThisFrame)
        {
            isFollowing = true;
            followTimer = 0f; // reset follow timer
        }

        // Follow player if key pressed OR if AI sees player
        if (isFollowing || seesPlayer)
        {
            followTimer += Time.deltaTime;

            if (target != null)
            {
                agent.SetDestination(target.position);
                agent.speed = followSpeed;
            }

            // Only stop following if it was the key-initiated follow AND duration passed
            if (isFollowing && followTimer >= followDuration)
            {
                agent.speed = originalSpeed;
                isFollowing = false;
            }
        }
        else
        {
            // Random roaming
            roamTimer += Time.deltaTime;
            if ((!agent.pathPending && agent.remainingDistance < 0.5f) || roamTimer >= roamDelay)
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
