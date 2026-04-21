using UnityEngine;

public class GrappleSystem : MonoBehaviour
{
    public Transform cam;
    public Rigidbody rb;

    public float grappleForce = 25f;
    public float maxDistance = 50f;
    public LayerMask grappleLayer;

    private Vector3 grapplePoint;
    private bool grappling;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            StartGrapple();

        if (grappling)
            GrappleMove();
    }

    void StartGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            grappling = true;
        }
    }

    void GrappleMove()
    {
        Vector3 dir = (grapplePoint - transform.position).normalized;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(dir * grappleForce, ForceMode.Impulse);

        if (Vector3.Distance(transform.position, grapplePoint) < 2f)
            grappling = false;
    }
}
