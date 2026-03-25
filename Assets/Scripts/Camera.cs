using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform cameraPosition;
    internal float fieldOfView;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
