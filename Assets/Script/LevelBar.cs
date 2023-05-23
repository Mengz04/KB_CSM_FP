using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelBar : MonoBehaviour{
    [SerializeField]
    private RectTransform curExp;

    [SerializeField]
    private Player player;

    [SerializeField]
    private TMP_Text levelCurTxt;

    private Vector3 scaleBar;
    
    void Awake(){
        scaleBar = new Vector3(0f, 20f, 0f);
    }

    void Update(){
        levelCurTxt.text = player.GetLevel().ToString();
        scaleBar.x = ((float)player.GetExp()/(float)player.GetMaxExp())*1920f;
        curExp.localScale = scaleBar;
    }
}
