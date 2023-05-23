using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour{
    private Animator animator;
    private SpriteRenderer rendOrder;

    void Awake(){
        animator = GetComponent<Animator>();
        rendOrder = GetComponent<SpriteRenderer>();
    }

    public void UpdateX(Vector2 direction){
        if(direction.x > 0.1f){
            animator.SetFloat("Horizontal", 1f);
        }
        else if (direction.x < -0.1f) {
            animator.SetFloat("Horizontal", -1f);
        }
        else{
            animator.SetFloat("Horizontal", 0f);
        }
    }

    public void UpdateY(Vector2 direction){
        if(direction.y > 0){
            animator.SetFloat("Vertical", 1f);
        }
        else if (direction.y < 0) {
            animator.SetFloat("Vertical", -1f);
        }
        else{
           animator.SetFloat("Vertical", 0f);
        }
    }

    public void AnimDead(){
        animator.SetFloat("Vertical", 0f);
        animator.SetFloat("Horizontal", 0f);
        animator.SetTrigger("isDead");
    }
}
