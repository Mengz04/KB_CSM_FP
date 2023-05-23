using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEXP : MonoBehaviour{

    public Transform circleOrigin;
    public float radius = 0.12f;

    private void Awake() {
        circleOrigin = GetComponentInChildren<Transform>();
    }
    private void Update(){
        DetectColliders();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Vector3 position = (circleOrigin == null)? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders(){
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius)){
            if(collider.gameObject.CompareTag("Player")){
                collider.GetComponent<Player>().AddExp(5);
                Destroy(gameObject);
            }
        }
    }
}
