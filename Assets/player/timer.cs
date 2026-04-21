using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public enum TriggerType { Start, End }
    public TriggerType triggerType;

    private TimerManager timerManager;

    void Start()
    {
        timerManager = FindObjectOfType<TimerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger: " + other.name);

        // Get Rigidbody from parent or child
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();

        if (rb == null)
        {
            Debug.Log("No Rigidbody found");
            return;
        }

        if (!rb.CompareTag("Player"))
        {
            Debug.Log("Not player");
            return;
        }

        Debug.Log("PLAYER DETECTED");

        if (triggerType == TriggerType.Start)
        {
            Debug.Log("START TRIGGERED");
            timerManager.StartTimer();
        }
        else
        {
            Debug.Log("END TRIGGERED");
            timerManager.StopTimer();
        }
    }
}
