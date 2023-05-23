using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour{
    public string damage = "";
    public TextMeshPro hud;
    public float despawnTime;
    public Color hudColor;

    void Start(){
        hud = GetComponent<TextMeshPro>();
        hudColor = hud.color;
        despawnTime = 1f;
    }
    
    void Update(){
        hud.SetText(damage);
        transform.position +=new Vector3(0f, 1f, 0f) * Time.deltaTime;

        despawnTime -= Time.deltaTime;
        if(despawnTime < 0f){
            hudColor.a -= 3f * Time.deltaTime;
            hud.color = hudColor;
            if(hudColor.a < 0){
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(string dmg){
        damage = dmg;
    }
}
