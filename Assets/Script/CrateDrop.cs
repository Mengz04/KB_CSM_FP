using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrateDrop : MonoBehaviour{
    [SerializeField]
    public Transform boxOrigin;

    [SerializeField]
    private TMP_Text buffName;

    private string buffType = "health";

    private void Awake() {
        if(Random.Range(0,3) == 0){buffType = "bombUp"; buffName.text = "Bomb Devil Upgrade";}
        else{buffName.text = "20 HP Heal";}
    }
    private void Update(){
        DetectColliders();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Vector3 position = (boxOrigin == null)? Vector3.zero : boxOrigin.position;
        boxOrigin.localScale = new Vector3(.6f, .35f, 0f);
        Gizmos.DrawWireCube(position, boxOrigin.localScale);
    }

    public void DetectColliders(){
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(boxOrigin.position, boxOrigin.localScale, 0f)){
            if(collider.gameObject.CompareTag("Player")){
                if(buffType == "bombUp"){
                    collider.GetComponentInChildren<SkillParent>().BombLevelUp();
                }
                else{
                    Health health;
                    if(health = collider.GetComponent<Health>()){
                        health.AddHealth(20);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
