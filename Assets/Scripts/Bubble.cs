using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Bubble : MonoBehaviour
{
    public float rangeMove;
    private Rigidbody2D body;
    public float maxTimeExits;
    private float timeExits;
    
    
    
    
    
     private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        maxTimeExits = 2.5f;
        timeExits = maxTimeExits / transform.localScale.x;
        Debug.Log("Time Exits " + timeExits);
    }

    private void Update()
    {
        float horizontalOffset = MathF.Sin(Time.time) * rangeMove;
        body.velocity = new Vector2(horizontalOffset, body.velocity.y);
        
        timeExits -= Time.deltaTime;
        if (timeExits <= 0)
        {
            Debug.Log("Exits");
            Destroy(this.gameObject);            
        }
    }

    
    
    
    
    
}
