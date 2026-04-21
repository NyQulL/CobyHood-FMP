using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float openDistance = 3f;
    public float openSpeed = 2f;
    public float moveAmount = 2f;

    private Transform player;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Vector3 targetPosition;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;

        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.down * moveAmount;

        targetPosition = closedPosition;
    }

    void Update()
    {
        // IMPORTANT: distance from fixed point
        float distanceToPlayer = Vector3.Distance(closedPosition, player.position);

        // Binary decision
        if (distanceToPlayer <= openDistance)
        {
            targetPosition = openPosition;
        }
        else
        {
            targetPosition = closedPosition;
        }

        // Smooth movement
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            openSpeed * Time.deltaTime
        );
    }
}
