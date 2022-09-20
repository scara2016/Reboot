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
    // Start is called before the first frame update
    void Start()
    {
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

        rb.velocity = new Vector3(movementVector.x*speed,0, movementVector.y*speed);
    }
}
