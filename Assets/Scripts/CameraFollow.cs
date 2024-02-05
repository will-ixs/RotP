using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetLocation;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothing;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Vector3 nextPosition = targetLocation.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, nextPosition, ref velocity, smoothing);         //Camera Damping
        //transform.position = nextPosition                                                                           //No camera damping
    }
}
