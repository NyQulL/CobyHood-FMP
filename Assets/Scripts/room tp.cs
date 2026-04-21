using UnityEngine;

public class RoomTeleport : MonoBehaviour
{
    [Header("Teleport Target")]
    [Tooltip("Where the player will be teleported to")]
    public Transform teleportDestination;

    [Header("Settings")]
    [Tooltip("Prevents instant re-teleporting")]
    public float teleportCooldown = 0.2f;

    private bool canTeleport = true;

    private void OnTriggerEnter(Collider other)
    {
        // Hard-coded player tag
        if (!canTeleport || !other.CompareTag("Player")) return;
        if (!teleportDestination) return;

        StartCoroutine(Teleport(other.transform));
    }

    private System.Collections.IEnumerator Teleport(Transform player)
    {
        canTeleport = false;

        // Handle CharacterController safely
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc) cc.enabled = false;

        // Teleport
        player.position = teleportDestination.position;
        player.rotation = teleportDestination.rotation;

        // Re-enable controller
        if (cc) cc.enabled = true;

        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}
