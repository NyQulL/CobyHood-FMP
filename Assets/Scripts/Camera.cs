using System;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform cameraPosition;
    internal float fieldOfView;

    internal void DOFieldOfView(float endValue, float v)
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
