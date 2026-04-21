using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float fuseTime = 2f;
    public float explosionRadius = 6f;
    public float explosionForce = 1200f;

    [Header("Boost Settings")]
    public float upwardBoostMultiplier = 0.6f;
    public float playerBoostMultiplier = 2f;

    public GameObject explosionEffect;

    void Start()
    {
        Invoke(nameof(Explode), fuseTime);
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, GetComponent<Rigidbody>().position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponentInParent<Rigidbody>();

            if (rb != null)
            {
                // Direction from explosion
                Vector3 direction = (rb.transform.position - transform.position).normalized;

                // Base force
                float force = explosionForce;

                // Extra boost if it's the player
                if (rb.CompareTag("Player"))
                {
                    force *= playerBoostMultiplier;
                }

                // Final force with upward boost
                Vector3 finalForce = direction * force + Vector3.up * (force * upwardBoostMultiplier);

                rb.AddForce(finalForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}

