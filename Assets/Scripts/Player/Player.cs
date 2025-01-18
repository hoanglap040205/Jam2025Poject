using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour,OndamgeAble
{
   public Rigidbody2D body;
   public Animator anim;
   public CapsuleCollider2D capsule;

   public float moveSpeed;
   public float jumpHeight;
   
   private float horizontalInput;


   public float distanceCheck;
   public Vector3 offsetCheckDownDir;
   
   private int facingright;
   
   
   //Gravity
   public float gravityScale = 1f;
   private float gravityImpactOvertime = 1f;
   private float maxGravityImpactOvertime = 3f;
   
   
   //Animator
   public readonly string jump = "Jump";
   public readonly string run = "Run";
   public readonly string grounded = "IsGrounded";
   public readonly string yVelocity = "YVelocity";
   public readonly string death = "Death";
   
   
   //
   public LayerMask platformLayer;
   
   
   //SpawmBubble
   public GameObject bubblePrefab;
   public bool canSpawm = true;
   public float timeCoolDowmSpawm;
   
   
   //Bubble
   
   //
   public Transform pointSpawm;
   public float radius;
   private Vector2 lookDir;
   
   
   ///Bubble
   private float timeRemainInput;
   private float defaultScaleBubble = 1.2f;
   private float scaleBubble;
   
   private void Awake()
   {
      body = GetComponent<Rigidbody2D>();
      capsule = GetComponent<CapsuleCollider2D>();
      anim = GetComponent<Animator>();
      currentHealth = StartHealth;

   }
   
   //Health
   public float StartHealth;
   public float currentHealth;
   
   private void Update()
   {
      horizontalInput = Input.GetAxisRaw("Horizontal");
      
      //Animator
      anim.SetBool(run,horizontalInput != 0 && isGrounded());
      anim.SetBool(grounded,isGrounded());
      anim.SetFloat(yVelocity,body.velocity.y);
      
      
      gravityImpactOvertime = !isGrounded() ? gravityImpactOvertime += Time.deltaTime : 0;
      
      
      Movement();
      RotateX();
      Jump();
      
      if (body.velocity.y >= 0)
      {
         body.gravityScale = gravityScale;
         
      }
      else
      {
         body.gravityScale = Mathf.Clamp(body.gravityScale + gravityImpactOvertime,gravityScale,maxGravityImpactOvertime );
      }
      timeCoolDowmSpawm -= Time.deltaTime;
      SpawmBubble();
      
   }

   private void RotateX()
   {
      Vector3 right = new Vector3(0, 0, 0);
      Vector3 left = new Vector3(0, 180, 0);
      if (horizontalInput != 0)
      {
         Vector3 rotate = horizontalInput > 0.01f ? right : left;
         facingright = horizontalInput > 0.1f ? 1 : -1;
         transform.rotation = Quaternion.Euler(rotate);

      }
   }

   private void Movement()
   {
      body.velocity = new Vector2(horizontalInput * moveSpeed , body.velocity.y);
   }

   private void Jump()
   {
      if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
      {
         anim.SetTrigger(jump);
         body.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
      }
      
   }

   

   private bool isGrounded()
   {
      bool isGrounded = Physics2D.CapsuleCast(transform.position + offsetCheckDownDir,capsule.size, CapsuleDirection2D.Vertical,0,Vector2.down,distanceCheck,platformLayer);
      return isGrounded;
   }


   private void SpawmBubble()
   {
      lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
      Vector2 look = lookDir.normalized * radius;
      pointSpawm.transform.position = transform.position + (Vector3)look;
      if (Input.GetMouseButton(0))
      {
         timeRemainInput += Time.deltaTime;
      }

      if (Input.GetMouseButtonUp(0))
      {
         timeRemainInput += 0.5f;
         scaleBubble = Mathf.Clamp(Mathf.Pow(defaultScaleBubble, timeRemainInput), 1.2f, 2.5f);
         Debug.Log(scaleBubble);
         if (timeCoolDowmSpawm <= 0)
         {
            timeRemainInput = 0;
            timeCoolDowmSpawm = 1.5f;
            GameObject bubble = Instantiate(bubblePrefab, pointSpawm.position, Quaternion.identity);
            Debug.Log(scaleBubble);
            bubble.transform.localScale = new Vector2(scaleBubble, scaleBubble);

         }

      }
      
   }
   
   

   private void OnDrawGizmos()
   {
      Gizmos.DrawRay(transform.position + offsetCheckDownDir, Vector2.down *distanceCheck);
      Gizmos.DrawWireSphere(transform.position,radius);
   }

   public void TakeDamage(float damage)
   {
      if (currentHealth > 0)
      {
         currentHealth = Mathf.Clamp(currentHealth - damage,0,StartHealth);  
      }
      else
      {
         
      }
   }
}