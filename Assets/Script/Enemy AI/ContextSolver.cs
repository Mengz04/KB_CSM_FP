using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSolver : MonoBehaviour
{
    [SerializeField]
    private bool showGizmos = true;

    float[] interestGizmo = new float[0];
    Vector2 resultDirection = Vector2.zero;
    private float rayLength = 2;

    private void Start(){
        interestGizmo = new float[8];
    }

    public Vector2 GetDirectionToMove(List<SteeringBehaviour> behaviours, AIInputData aiData){
        float[] danger = new float[8];
        float[] interest = new float[8];

        // Loop ke obstacle avoidance behaviour dan seek behaviour
        foreach (SteeringBehaviour behaviour in behaviours){
            (danger, interest) = behaviour.GetSteering(danger, interest, aiData);
        }

        // Override nilai interest dengan pengurangan interest-danger sebuah direction (clamp 01)
        for (int i = 0; i < 8; i++){
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
        }

        interestGizmo = interest;

        // Perkalian movement direction dengan final interest
        Vector2 outputDirection = Vector2.zero;
        for (int i = 0; i < 8; i++){
            outputDirection += Directions.eightDirections[i]*interest[i];
        }

        // Vektor dinormalkan (magnitude == 1)
        outputDirection.Normalize();

        return outputDirection;
    }


    private void OnDrawGizmos(){
        if (Application.isPlaying && showGizmos){
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, resultDirection * rayLength);
        }
    }
}