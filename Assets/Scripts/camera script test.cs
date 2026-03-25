using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Mouse")]
    public float sensX = 520f;
    public float sensY = 480f;

    [Header("Normal Camera")]
    public float distance = 2.8f;
    public float height = 0.9f; // 🔥 MUCH LOWER
    public float sideOffset = 0.75f;

    [Header("Aim Camera")]
    public float aimDistance = 1.6f;
    public float aimHeight = 1.0f; // 🔥 STILL LOW
    public float aimSideOffset = 0.35f;

    [Header("Snappiness")]
    public float followSpeed = 28f;
    public float rotationSpeed = 32f;

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
        UpdateCamera();
    }

    void UpdateCamera()
    {
        float targetDist = isAiming ? aimDistance : distance;
        float targetHeight = isAiming ? aimHeight : height;
        float targetSide = (isAiming ? aimSideOffset : sideOffset) * shoulder;

        currentDistance = Mathf.Lerp(currentDistance, targetDist, Time.deltaTime * 20f);
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * 20f);
        currentSide = Mathf.Lerp(currentSide, targetSide, Time.deltaTime * 20f);

        Quaternion rot = Quaternion.Euler(xRot, yRot, 0);

        Vector3 offset = rot * new Vector3(currentSide, currentHeight, -currentDistance);
        Vector3 targetPos = target.position + offset;

        // 🔥 SNAPPY POSITION
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);

        // 🔥 LOOK SLIGHTLY ABOVE CHEST (FORTNITE STYLE)
        Vector3 lookPoint = target.position + Vector3.up * 1.1f;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(lookPoint - transform.position),
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
