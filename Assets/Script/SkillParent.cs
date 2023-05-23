using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillParent : MonoBehaviour
{
    [SerializeField]
    private bool ownBombDvl = true, ownGunDvl = false;
    
    [SerializeField]
    private float bombInterval= 15f, gunInterval = 15f;

    private int bombLevel = 1;

    [SerializeField]
    private GameObject bombGrenade, bulletGun;

    [SerializeField]
    private BombSkillHUD hud;

    [SerializeField] private TMP_Text hudCurLevel;

    private bool bombCoroutStart = false, gunCoroutStart = false;

    public void BombLevelUp(){
        if(ownBombDvl == false){ownBombDvl = true; return;}
        if(bombLevel>=10) return;
        bombLevel++;
        hudCurLevel.text = bombLevel.ToString();
    }

    void Update(){
        if(ownBombDvl== true && bombCoroutStart == false){
            bombCoroutStart = true;
            StartCoroutine(spawnBomb(bombGrenade));
        }
    }

    private IEnumerator spawnBomb(GameObject bomb){
        hud.CooldownCounter(bombInterval-bombLevel*0.8f);
        yield return new WaitForSeconds(bombInterval-bombLevel*0.8f);
        GameObject newEnemy = Instantiate(bomb, new Vector3(transform.position.x,  transform.position.y, 0), Quaternion.identity);
        StartCoroutine(spawnBomb(bomb));
    }
}
