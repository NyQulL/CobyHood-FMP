using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float sensX = 520f;
    public float sensY = 480f;

    public float distance = 4.5f;
    public float height = 1.5f;
    public float sideOffset = 1.2f;

    public float aimDistance = 2.2f;
    public float aimHeight = 1.4f;
    public float aimSideOffset = 0.6f;

    public float followSpeed = 12f;
    public float rotationSpeed = 16f;

    float yRot;
    float xRot;

    bool isAiming;
    int shoulder = 1;

    float currentDistance;
    float currentHeight;
    float currentSide;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        currentDistance = distance;
        currentHeight = height;
        currentSide = sideOffset;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRot += mouseX;
        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, -25f, 65f);

        isAiming = Input.GetMouseButton(1);

        if (Input.GetKeyDown(KeyCode.Q)) shoulder = -1;
        if (Input.GetKeyDown(KeyCode.E)) shoulder = 1;
    }

    void LateUpdate()
    {
        float targetDist = isAiming ? aimDistance : distance;
        float targetHeight = isAiming ? aimHeight : height;
        float targetSide = (isAiming ? aimSideOffset : sideOffset) * shoulder;

        currentDistance = Mathf.Lerp(currentDistance, targetDist, Time.deltaTime * 10f);
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * 10f);
        currentSide = Mathf.Lerp(currentSide, targetSide, Time.deltaTime * 10f);

        Quaternion rotation = Quaternion.Euler(xRot, yRot, 0);

        Vector3 pos = target.position
                      - rotation * Vector3.forward * currentDistance
                      + Vector3.up * currentHeight;

        pos += rotation * Vector3.right * currentSide;

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * followSpeed);

        Vector3 lookTarget = target.position + Vector3.up * 1.4f;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(lookTarget - transform.position),
            Time.deltaTime * rotationSpeed
        );
    }

    public float GetYRotation()
    {
        return yRot;
    }

    public bool IsAiming()
    {
        return isAiming;
    }
}

