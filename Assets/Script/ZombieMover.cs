using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private ZombieAnimator zombieAnimator;

    [SerializeField]
    public Transform circleOrigin;
    public float radius = 1f;

    private float maxSpeed = 1, acceleration= 25, deacceleration = 100;

    private float lifeTime = 30f;
    private float currentSpeed=0;
    private GameObject player;

    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    private bool isDead = false;

    private void Start(){
        gameObject.tag = "Zombie";
        player = GameObject.FindGameObjectWithTag("Player");

        zombieAnimator = GetComponent<ZombieAnimator>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        StartCoroutine(LifeCountDown());
        StartCoroutine(CollideCountdown());
    }

    private void FixedUpdate(){
        if(isDead == false){
            if(player == null) return;
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
            rb.velocity = Vector2.zero;
        }
    }

    

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Vector3 position = (circleOrigin == null)? Vector3.zero : circleOrigin.position;
        circleOrigin.localScale = new Vector3(.4f, 1f, 0f);
        Gizmos.DrawWireCube(position, circleOrigin.localScale);
    }

    private IEnumerator LifeCountDown(){
        yield return new WaitForSeconds(lifeTime);
        SetDead();
    }

    public void SetDead(){
        if(gameObject == null) return;
        isDead = true;
        zombieAnimator.AnimDead();
        StartCoroutine(DespawnCountdown(.5f));
    }

    public IEnumerator DespawnCountdown(float interval){
        yield return new WaitForSeconds(interval);
        if(gameObject != null) Destroy(gameObject);
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
}
