using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGrenade : MonoBehaviour{
    [SerializeField]
    private GameObject explosion;

    private float fuse = 8f;
    public Transform circleOrigin;
    public float radius = 0.12f;

    private void Awake() {
        circleOrigin = GetComponentInChildren<Transform>();
        StartCoroutine(FuseCountdown(fuse));
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
            if(collider.gameObject.CompareTag("Zombie") || collider.gameObject.CompareTag("Zomdev")){
                Explode();
            }
        }
    }

    private IEnumerator FuseCountdown(float interval){
        yield return new WaitForSeconds(interval);
        Explode();
    }

    private void Explode(){
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
