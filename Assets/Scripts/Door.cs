using UnityEngine;
using TMPro;

public class DoorInteract : MonoBehaviour
{
    public string requiredKeyID;
    public Transform door;
    public float openHeight = 3f;
    public float openSpeed = 2f;
    public float interactDistance = 3f;
    public TextMeshProUGUI interactText;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        closedPos = door.position;
        openPos = door.position + Vector3.up * openHeight;
        interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactDistance)
        {
            interactText.gameObject.SetActive(true);

            bool hasKey = PlayerKeys.Instance.HasKey(requiredKeyID);

            interactText.text = hasKey ? "Press E to Open" : "Key Required";

            if (Input.GetKeyDown(KeyCode.E) && hasKey && !isOpen)
            {
                isOpen = true;
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }

        if (isOpen)
        {
            door.position = Vector3.Lerp(door.position, openPos, Time.deltaTime * openSpeed);
        }
    }
}

