using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour{
    [SerializeField]
    private RectTransform curHP;

    [SerializeField]
    private GameObject player;
    private Vector3 scaleBar;
    private Health playerHealth;
    
    void Awake(){
        playerHealth = player.GetComponent<Health>();
        scaleBar = new Vector3(320f, 20f, 0f);
    }

    void Update(){
        scaleBar.x = (((float)playerHealth.GetCurrentHealth())/((float)playerHealth.GetMaxHealth()))*320f;
        curHP.localScale = scaleBar;
    }
}
