using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    [SerializeField]
    private GameObject bloodEXP;

    [SerializeField]
    private GameObject CrateDrop;

    [SerializeField]
    private GameObject damageNumber;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    private bool isDead = false;

    public void InitializeHealth(int healthValue){
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public int GetCurrentHealth(){
        return currentHealth;
    }
    
    public int GetMaxHealth(){
        return maxHealth;
    }

    public void AddHealth(int val){
        if(isDead) return;
        
        if(currentHealth + val >= maxHealth){currentHealth = maxHealth;}
        else{currentHealth += val;}
    }

    public void GetHit(int amount, GameObject sender){
        if(isDead) return;
        if(gameObject == null) return;
        if(sender.layer == gameObject.layer) return;

        currentHealth -= amount;

        //add dmg hud
        GameObject dmgtext =  Instantiate(damageNumber, transform.position, Quaternion.identity) as GameObject;
        dmgtext.GetComponent<DamageText>().SetDamage(amount.ToString());

        if(currentHealth > 0){
            OnHitWithReference?.Invoke(sender);
        }
        else{
            isDead = true;
            OnDeathWithReference?.Invoke(sender);
            if(gameObject.CompareTag("Zombie")){
                if(gameObject != null){ZombieDead();}
            }
            else if(gameObject.CompareTag("Player")){
                isDead = true;
                if(gameObject != null){gameObject.GetComponent<PlayerMover>().SetDead();}
            }
            else if(gameObject.CompareTag("Zomdev")){
                if(gameObject != null){ZomdevDead();}
            }
        }
    }

    public void FlatHit(int amount){
        if(isDead) return;
        if(gameObject == null) return;
        currentHealth -= amount;

        //add dmg hud
        GameObject dmgtext =  Instantiate(damageNumber, transform.position, Quaternion.identity) as GameObject;
        dmgtext.GetComponent<DamageText>().SetDamage(amount.ToString());

        if(currentHealth <= 0){
            isDead = true;
            if(gameObject.CompareTag("Zombie")){
                if(gameObject != null){ZombieDead();}
            }
            else if(gameObject.CompareTag("Player")){
                isDead = true;
                if(gameObject != null){gameObject.GetComponent<PlayerMover>().SetDead();}
            }
            else if(gameObject.CompareTag("Zomdev")){
                if(gameObject != null){ZomdevDead();}
            }
        }
    }

    private void ZombieDead(){
        if(gameObject == null) return;
        Instantiate(bloodEXP, transform.position, Quaternion.identity);
        if(Random.Range(0, 2) == 0){Instantiate(CrateDrop, transform.position, Quaternion.identity);}
        isDead = true;
        if(gameObject != null){gameObject.GetComponent<ZombieMover>().SetDead();}
    }

    private void ZomdevDead(){
        if(gameObject == null) return;
        Instantiate(bloodEXP, transform.position, Quaternion.identity);
        Instantiate(CrateDrop, transform.position, Quaternion.identity);
        isDead = true;
        if(gameObject != null){gameObject.GetComponent<ZomdevMover>().SetDead();}
    }
}
