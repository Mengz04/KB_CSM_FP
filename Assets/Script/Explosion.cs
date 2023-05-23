using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour{
    public Transform circleOrigin;
    public float radius = 0.48f;

    void Awake(){
        circleOrigin = GetComponentInChildren<Transform>();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Vector3 position = (circleOrigin == null)? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders(){
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius)){
            if(collider.gameObject.CompareTag("Zombie") || collider.gameObject.CompareTag("Zomdev")){
                Health health;
                if(health = collider.GetComponent<Health>()){
                    health.FlatHit(8);
                }
            }
        }
    }

    private void Despawn(){
        Destroy(gameObject);
    }
}
