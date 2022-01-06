using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector]
    public Vector3 targetPos;

    private float smoothMove = 1.0f;

    private void Start()
    {
        // initial position of the camera
        targetPos = transform.position;
    }
    void Update()
    {
        //Smoothly move the camera linearly
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothMove * Time.deltaTime);    
    }
}
