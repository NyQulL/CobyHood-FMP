using UnityEngine;

public class PlayerGrenadeThrow : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;

    public float throwForce = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(throwPoint.forward * throwForce, ForceMode.VelocityChange);
    }
}
