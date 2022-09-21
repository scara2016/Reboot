using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rb;
    private Vector2 movementVector;
    [SerializeField]
    private float speed;
    public float speedModifier=1;
    private int grabbed;
    private Dispenser dispenser;
    private tillScript till;
    private PlayerMeters playerMeters;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMeters = GetComponent<PlayerMeters>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        movementVector = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

   void OnMovement(InputValue value)
   {
        movementVector = value.Get<Vector2>();
   }
    

    void Movement()
    {

        rb.velocity = (new Vector3(movementVector.x*speed,0, movementVector.y*speed))*speedModifier;
    }
    void OnGrab()
    {
        Grab();
    }

    private void Grab()
    {
        if (dispenser != null)
        {
            if (!dispenser.OnCoolDown)
            {
                grabbed = dispenser.goalColorInt;
                if (grabbed == 0)
                {
                    this.GetComponent<MeshRenderer>().material.color = Color.blue;
                }
                else if (grabbed == 1)
                {
                    this.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                else
                {
                    this.GetComponent<MeshRenderer>().material.color = Color.black;
                }
            }
            dispenser.OnCoolDown = true;
        }
        if (till != null)
        {
            if (till.CustomerCheck(grabbed))
            {
                grabbed = -1;
                this.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            
        }
        
    }
    void OnConsume()
    {
        Consume();
    }

    private void Consume()
    {
        if (grabbed == 0)
        {
            playerMeters.BlueMeter = 100;
            this.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else if (grabbed == 1)
        {
            playerMeters.GreenMeter = 100;
            this.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Dispenser")
        {
           dispenser = other.transform.GetComponent<Dispenser>();
        }
        if (other.transform.tag == "Till")
        {
            till = other.transform.GetComponent<tillScript>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Dispenser")
        {
            dispenser = null;
        }
        if (other.transform.tag == "Till")
        {
            till = null;
        }
    }
}
