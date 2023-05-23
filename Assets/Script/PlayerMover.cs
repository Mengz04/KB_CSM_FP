using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;

    [SerializeField]
    private float maxSpeed = 2, acceleration= 50, deacceleration = 100;

    [SerializeField]
    private float currentSpeed=0;

    private Vector2 oldMovementInput;
    private bool isDead = false;

    public void SetDead(){
        isDead = true;
        player.PlayerSetDead();
    }

    public Vector2 MovementInput {get; set;}

    private void Awake(){
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        if(!isDead){
            UpdateX();
            UpdateY();
        
            if(MovementInput.magnitude > 0 && currentSpeed >= 0){
                oldMovementInput = MovementInput;
                currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            }
            else{
                currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
            }

            currentSpeed = Mathf.Clamp(currentSpeed, 0 , maxSpeed);
            rb.velocity = oldMovementInput * currentSpeed;
        }
    }

    private void UpdateX(){
        if(MovementInput.x > 0){
            player.animator.SetFloat("Horizontal", 1f);
        }
        else if (MovementInput.x < 0) {
            player.animator.SetFloat("Horizontal", -1f);
        }
        else{
            player.animator.SetFloat("Horizontal", 0f);
        }
    }

    private void UpdateY(){
        if(MovementInput.y > 0){
            player.animator.SetFloat("Vertical", 1f);
        }
        else if (MovementInput.y < 0) {
            player.animator.SetFloat("Vertical", -1f);
        }
        else{
            player.animator.SetFloat("Vertical", 0f);
        }
    }
}
