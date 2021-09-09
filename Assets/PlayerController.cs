using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Photon.Pun;


[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    

    [Header("PlayerSettings")]
    [Space(2)]
    [Tooltip("Speed value must be between 1 and 6.")]
    [Range(1.0f, 6.0f)]
    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravity;
    

    public int maxHealth = 5;
    public int currentHealth;
  

    private float timeBtwShots;
    public float startTimeBtwShots;
    

    public Material[] Materials;
    public Renderer TargetRenderer;


    Vector3 moveDirection;
    GameObject player;

    enum ControllerType { SimpleMove, Move };
    [SerializeField] ControllerType type;

    [Header("Weapon Settings")]
    public float projectileForce;
    

    PhotonView view;

    void Start()
    {

        controller = GetComponent<CharacterController>();
        

        controller.minMoveDistance = 0.0f;

        

        name = "Player";

        currentHealth = maxHealth;
       
        view = GetComponent<PhotonView>();

        
            if (speed <= 0)
            {
                speed = 6.0f;

                Debug.LogWarning(name + ": Speed not set. Defaulting to " + speed);
            }

            if (jumpSpeed <= 0)
            {
                jumpSpeed = 6.0f;
                

                Debug.LogWarning(name + ": jumpSpeed not set. Defaulting to " + jumpSpeed);
            }

            if (rotationSpeed <= 0)
            {
                rotationSpeed = 10.0f;

                Debug.LogWarning(name + ": rotationSpeed not set. Defaulting to " + rotationSpeed);
            }

            if (gravity <= 0)
            {
                gravity = 6.0f;

                Debug.LogWarning(name + ": gravity not set. Defaulting to " + gravity);
            }

            if (projectileForce <= 0)
            {
                projectileForce = 10.0f;

                Debug.LogWarning(name + ": projectileForce not set. Defaulting to " + projectileForce);
            }

          

            moveDirection = Vector3.zero;
        

    }

    void Update()
    {
       


        switch (type)
        {

            case ControllerType.SimpleMove:

                

                controller.SimpleMove(transform.forward * Input.GetAxis("Vertical") * speed);

                break;

            case ControllerType.Move:

                if (controller.isGrounded)
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                    moveDirection *= speed;

                    moveDirection = transform.TransformDirection(moveDirection);

                    if (Input.GetButtonDown("Jump"))
                        moveDirection.y = jumpSpeed;
                }

                moveDirection.y -= gravity * Time.deltaTime;

                controller.Move(moveDirection * Time.deltaTime);

                break;
        }

        


        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Spawning Wave");
        }






    }

    [ContextMenu("Reset Stats")]
    void ResetStats()
    {
        speed = 6.0f;
    }


    

 

    void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "Hazard")
        {
            TakeDamage(1);
        }

       

        void TakeDamage(int damage)
        {
            currentHealth -= damage;

           
        }

        
    }



}