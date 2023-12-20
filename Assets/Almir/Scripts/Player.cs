using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool dance;
    [SerializeField] private float speedR = 2f;
    [SerializeField] private GameInput gameInput;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInter();
    }

    private void HandleMovement(){
        
        Vector2 movementVector = gameInput.GetMovementVector();
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        var vel = inputVector * speed;
        animator.SetFloat("Speed", vel.magnitude);
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 2f , .5f, inputVector);
        if(canMove){
            transform.position += vel * Time.deltaTime;
            transform.forward = Vector3.Slerp( transform.forward, inputVector, Time.deltaTime * speedR); 
        }
    }

    private void HandleInter(){
        Vector2 movementVector = gameInput.GetMovementVector();
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);

        if(Physics.Raycast(transform.position, inputVector, out RaycastHit raycastHit, 2f)){
            Debug.Log(raycastHit.transform);
        }
    }

    public float getSpeed(){
        return speed;
    }

    public void setSpeed(float value){
        speed = value;
    }

    public bool getDancing(){
        return dance;
    }

    public void setDancing(bool value){
        dance = value;
        animator.SetBool("dance", dance);
    }
}
