using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehaviour : SteeringBehaviour{

    // *Butuh adjustment lanjutan
    [SerializeField]
    private float radius = 2f, agentColliderSize = 1.7f;

    [SerializeField]
    private bool showGizmo = true;

    float[] dangersResultTemp = null;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIInputData aiData){
        // Untuk setiap collider yang didetect berada pada layer obstacle
        foreach (Collider2D obstacleCollider in aiData.obstacles){
            // vektor arah obstacle dari zombie
            Vector2 directionToObstacle = obstacleCollider.ClosestPoint(transform.position)-(Vector2)transform.position;
            // Mendapatkan jarak obstacle dari zombie
            float distanceToObstacle = directionToObstacle.magnitude;

            // Weight berdasarkan jarak zombie thd obstacle
            float weight = (distanceToObstacle <= agentColliderSize) ? 1 : (radius - distanceToObstacle) / radius;

            // vektor dinormalkan (magnitude == 1)
            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

            // kalkulasi nilai danger[] thd masing-masing arah gerak
            for (int i = 0; i < Directions.eightDirections.Count; i++){
                // Perkalian dot prod. vektor arah ke obstacle dan vektor arah gerak player
                float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]);

                // Evaluasi dengan weight (koefisien berdasarkan jarak zombie thd obstacle)
                float valueToPutIn = result * weight;

                // override hanya jika nilainya lebih besar dari yang sudah tersimpan pada array danger
                if (valueToPutIn > danger[i]){ danger[i] = valueToPutIn; }
            }
        }
        dangersResultTemp = danger;
        return (danger, interest);
    }

    private void OnDrawGizmos(){
        if (showGizmo == false)
            return;

        if (Application.isPlaying && dangersResultTemp != null)
        {
            if (dangersResultTemp != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangersResultTemp.Length; i++)
                {
                    Gizmos.DrawRay(
                        transform.position,
                        Directions.eightDirections[i] * dangersResultTemp[i]*2
                        );
                }
            }
        }

    }
}

public static class Directions
{
    public static List<Vector2> eightDirections = new List<Vector2>{
            new Vector2(0,1).normalized,
            new Vector2(1,1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,1).normalized
        };
}