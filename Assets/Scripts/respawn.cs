using System.Collections;
using UnityEngine;

public class FallResetCharacterController : MonoBehaviour
{
    public float fallY = -10f;
    public float fadeSpeed = 2f;
    public CanvasGroup fadeCanvas;

    private Vector3 lastSafePosition;
    private bool isResetting = false;
    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        lastSafePosition = transform.position;
        fadeCanvas.alpha = 0f;
    }

    void Update()
    {
        // ✅ SAVE POSITION ONLY WHEN SAFELY GROUNDED
        if (cc.isGrounded && !isResetting)
        {
            lastSafePosition = transform.position;
        }

        // ❌ FALL DETECTION
        if (transform.position.y <= fallY && !isResetting)
        {
            StartCoroutine(ResetPlayer());
        }
    }

    IEnumerator ResetPlayer()
    {
        isResetting = true;

        // Fade to black
        while (fadeCanvas.alpha < 1f)
        {
            fadeCanvas.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // Teleport safely
        cc.enabled = false;
        transform.position = lastSafePosition;
        cc.enabled = true;

        // Fade back in
        while (fadeCanvas.alpha > 0f)
        {
            fadeCanvas.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        isResetting = false;
    }
}

