using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyID; // e.g. "RedKey", "BasementKey"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKeys.Instance.AddKey(keyID);
            Destroy(gameObject);
        }
    }
}

