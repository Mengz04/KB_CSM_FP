using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZomdevMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private ZombieAnimator zombieAnimator;

    [SerializeField]
    public Transform circleOrigin;
    public float radius = 1f;

    private float maxSpeed = 1, acceleration= 25, deacceleration = 100;

    private float lifeTime = 120f;
    private float currentSpeed=0;
    private GameObject player;

    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    private bool isDead = false;

    private void Awake(){
        gameObject.tag = "Zomdev";
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().Tracked();
        zombieAnimator = GetComponent<ZombieAnimator>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        StartCoroutine(LifeCountDown());
        StartCoroutine(CollideCountdown());
    }

    private void FixedUpdate(){
        if(isDead == false){
            if(player == null) return;
            player.GetComponent<Player>().Tracked();
            AllFatherBroadcast();
            zombieAnimator.UpdateX(MovementInput);
            zombieAnimator.UpdateY(MovementInput);

            if (MovementInput.magnitude > 0 && currentSpeed >= 0){
                oldMovementInput = MovementInput;
                currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            }
            else{
                currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
            }
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            rb.velocity = oldMovementInput * currentSpeed;
        }
        else{
            AllFatherStop();
            rb.velocity = Vector2.zero;
        }
    }

    

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Vector3 position = (circleOrigin == null)? Vector3.zero : circleOrigin.position;
        circleOrigin.localScale = new Vector3(1.3f, 1.7f, 0f);
        Gizmos.DrawWireCube(position, circleOrigin.localScale);
    }

    private IEnumerator LifeCountDown(){
        yield return new WaitForSeconds(lifeTime);
        SetDead();
    }

    public void SetDead(){
        player.GetComponent<Player>().Untracked();
        if(gameObject == null) return;
        isDead = true;
        Destroy(gameObject);
    }

    public IEnumerator CollideCountdown(){
        yield return new WaitForSeconds(.5f);
        DetectColliders();
        StartCoroutine(CollideCountdown());
    }


    public void DetectColliders(){
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(circleOrigin.position, circleOrigin.localScale, 0f)){
            if(collider.gameObject.CompareTag("Player")){
                Health health;
                health = collider.GetComponent<Health>();
                health.FlatHit(1);
                break;
            }
        }        
    }

    private void AllFatherBroadcast(){
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie")) {
            zombie.GetComponentInChildren<TargetDetector>().AllFatherDetect(player.transform);
            zombie.GetComponentInChildren<SeekBehaviour>().AllFatherOn(player.transform);
        }
        foreach (GameObject zomdev in GameObject.FindGameObjectsWithTag("Zomdev")) {
            zomdev.GetComponentInChildren<TargetDetector>().AllFatherDetect(player.transform);
            zomdev.GetComponentInChildren<SeekBehaviour>().AllFatherOn(player.transform);
        }
    }
    private void AllFatherStop(){
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie")) {
            zombie.GetComponentInChildren<TargetDetector>().AllFatherStopDetect();
            zombie.GetComponentInChildren<SeekBehaviour>().AllFatherOff();
        }
        foreach (GameObject zomdev in GameObject.FindGameObjectsWithTag("Zomdev")) {
            zomdev.GetComponentInChildren<TargetDetector>().AllFatherStopDetect();
            zomdev.GetComponentInChildren<SeekBehaviour>().AllFatherOff();
        }
    }
}
