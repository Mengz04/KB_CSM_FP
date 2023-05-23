using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 pointerPosition{ get; set; }
    private Player player;
    public Animator animator {get; set;}
    public float coolDown = 0.5f;
    private bool attackBlocked = false;
    public bool isAttacking {get; private set;}

    

    public Transform circleOrigin;
    public float radius;

    public void resetIsAttacking(){
        isAttacking = false;
    }
    
    private void Awake(){
        player = GetComponentInParent<Player>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update(){
        if(isAttacking) return;
        transform.right = (pointerPosition - (Vector2)transform.position).normalized;

        Vector3 scale = transform.localScale;
        float dotProduct = Vector2.Dot(Vector2.right, transform.right);
        if(dotProduct<0){
            scale.y = -1;
        }
        else if(dotProduct > 0){
            scale.y = 1;
        }
        transform.localScale = scale;
    }

    public void Attack(){
        if(attackBlocked) return;
        animator.SetTrigger("Attack");
        isAttacking = true;
        attackBlocked = true;
        StartCoroutine(coolDownCounter());
    }

    private IEnumerator coolDownCounter(){
        yield return new WaitForSeconds(coolDown);
        attackBlocked = false;
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders(){
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius)){
            Health health;
            if(health = collider.GetComponent<Health>()){
                health.GetHit(1, transform.parent.gameObject);
            }
        }
    }
}
