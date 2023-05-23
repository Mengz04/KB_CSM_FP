using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkillHUD : MonoBehaviour{
    [SerializeField]
    private RectTransform curCD;

    [SerializeField]
    private Vector3 scaleBar;
    void Awake(){
        scaleBar = new Vector3(0.6f, 0.6f, 0f);
    }

    public void CooldownCounter(float interval){
        StartCoroutine(StartCooldownCounter(interval, interval));
    }
    public IEnumerator StartCooldownCounter(float remaining, float max){
        yield return new WaitForSeconds(.1f);
        remaining -= .1f;
        scaleBar.x = ((max-remaining)/max)*0.6f;
        curCD.localScale = scaleBar;
        if(remaining > 0.1){StartCoroutine(StartCooldownCounter(remaining, max));}
        else{
            scaleBar.x = 0.6f;
            curCD.localScale = scaleBar;
        }
    }
}
