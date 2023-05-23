using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector{
    [SerializeField] 
    private float targetDetectionRange = 5f;

    [SerializeField]
    private LayerMask obstaclesLayerMask, playerLayerMask;

    [SerializeField]
    private bool showGizmos = false;
    private List<Transform> colliders;

    private bool AllFatherDetectStat = false;

    public override void Detect(AIInputData aiData){
        if(AllFatherDetectStat == false){
            Collider2D playerCollider = 
                Physics2D.OverlapCircle(transform.position, targetDetectionRange, playerLayerMask);

            if (playerCollider != null){
                // Jika ditemukan collider player
                Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
                RaycastHit2D hit = 
                    Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstaclesLayerMask);

                // Pastikan player ada pada layer "Player" (just in case)
                if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0){
                    Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                    colliders = new List<Transform>() { playerCollider.transform };
                }
                else{
                    colliders = null;
                }
            }
            else{
                // Tidak ditemukan collider player
                colliders = null;
            }
        }
        aiData.targets = colliders;
    }

    private void OnDrawGizmosSelected() {
        if (showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

        if (colliders == null)
            return;
        Gizmos.color = Color.magenta;
        foreach (var item in colliders){
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }

    public void AllFatherDetect(Transform playerPos){
        AllFatherDetectStat = true;
        colliders = new List<Transform>() { playerPos };
    }

    public void AllFatherStopDetect(){
        AllFatherDetectStat = false;
    }
}