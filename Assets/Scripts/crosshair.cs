using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [Header("Crosshair Parts")]
    public RectTransform top;
    public RectTransform bottom;
    public RectTransform left;
    public RectTransform right;

    [Header("Settings")]
    public float baseGap = 8f;
    public float maxGap = 25f;

    public float moveExpand = 12f;
    public float shootExpand = 18f;

    public float smoothSpeed = 10f;

    [Header("References")]
    public Rigidbody playerRb;

    float currentGap;
    float targetGap;

    void Start()
    {
        currentGap = baseGap;
    }

    void Update()
    {
        HandleSpread();
        UpdateCrosshair();
    }

    void HandleSpread()
    {
        float speed = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z).magnitude;

        float moveAmount = speed * 0.5f;

        // Shooting expansion
        if (Input.GetMouseButton(0))
        {
            targetGap = baseGap + moveAmount + shootExpand;
        }
        else
        {
            targetGap = baseGap + moveAmount;
        }

        targetGap = Mathf.Clamp(targetGap, baseGap, maxGap);

        currentGap = Mathf.Lerp(currentGap, targetGap, Time.deltaTime * smoothSpeed);
    }

    void UpdateCrosshair()
    {
        top.anchoredPosition = new Vector2(0, currentGap);
        bottom.anchoredPosition = new Vector2(0, -currentGap);
        left.anchoredPosition = new Vector2(-currentGap, 0);
        right.anchoredPosition = new Vector2(currentGap, 0);
    }
}
