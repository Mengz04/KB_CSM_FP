using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour{
    // last position system
    [SerializeField]
    private float targetRechedThreshold = 0.5f;

    [SerializeField]
    private bool showGizmo = true;

    bool reachedLastTarget = true;

    bool allFatherStat = false;

    private Transform playerPos;

    //gizmo parameters
    private Vector2 targetPositionCached;
    private float[] interestsTemp;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIInputData aiData) {
        // Jika player out of sight stop seek
        // Set target baru (posisi player last seen)
        if (reachedLastTarget){
            if (aiData.targets == null || aiData.targets.Count <= 0){
                aiData.currentTarget = null;
                return (danger, interest);
            }
            else{
                reachedLastTarget = false;
                // Set current target berdasarkan target terdekat dengan zombie
                if(allFatherStat == true){
                    aiData.currentTarget = playerPos;
                }
                else{
                    aiData.currentTarget = aiData.targets.OrderBy(target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
                }
            }

        }

        // Simpan last position player jika masih in sight
        if (aiData.currentTarget != null && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget))
            targetPositionCached = aiData.currentTarget.position;

        // Cek sampai di target atau belum
        if (Vector2.Distance(transform.position, targetPositionCached) < targetRechedThreshold){
            reachedLastTarget = true;
            aiData.currentTarget = null;
            return (danger, interest);
        }

        // Jika belum sampai
        Vector2 directionToTarget = (targetPositionCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++){
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            // Filter hanya hasil dot prod positif
            if (result > 0){
                if (result > interest[i]){
                    interest[i] = result;
                }
            }
        }
        interestsTemp = interest;
        return (danger, interest);
    }

    public void AllFatherOn(Transform input){
        allFatherStat = true;
        playerPos = input;
        //Debug.Log(playerPos);
    }

    public void AllFatherOff(){
        allFatherStat = false;
    }

    private void OnDrawGizmos(){

        if (showGizmo == false)return;
        Gizmos.DrawSphere(targetPositionCached, 0.2f);

        if (Application.isPlaying && interestsTemp != null){
            if (interestsTemp != null){
                Gizmos.color = Color.green;
                for (int i = 0; i < interestsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestsTemp[i]*2);
                }
                if (reachedLastTarget == false)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targetPositionCached, 0.1f);
                }
            }
        }
    }
}