using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject player;
    public float smoothTime = 0.1f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        
        transform.position = Vector3.SmoothDamp(transform.position, (Vector3)player.transform.position + offset, ref velocity, smoothTime);
    }
}
