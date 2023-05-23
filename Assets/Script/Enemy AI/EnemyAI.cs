using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour{
    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;
    
    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIInputData aiData;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f;

    // movement input dikirim ke ZombieMover Script
    public UnityEvent<Vector2> OnMovementInput;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextSolver movementDirectionSolver;

    bool following = false;

    private void Start(){
        // Detect obstacle sekitar radius
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection(){
        foreach (Detector detector in detectors){
            detector.Detect(aiData);
        }

    }

    private void Update(){
        // Movement berdasarkan target availability
        if (aiData.currentTarget != null){
            if (following == false){
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0){
            // Logic assignment current target
            aiData.currentTarget = aiData.targets[0];
        }
        // Move agent
        OnMovementInput?.Invoke(movementInput);
    }

    private IEnumerator ChaseAndAttack(){
        if (aiData.currentTarget == null){
            //Debug.Log("Stopping");
            movementInput = Vector2.zero;
            following = false;
            yield break;
        }
        else{
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);
            // Kejar target
            movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
            yield return new WaitForSeconds(aiUpdateDelay);
            StartCoroutine(ChaseAndAttack());

        }

    }
}