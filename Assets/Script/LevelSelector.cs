using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour{
    private int bestLevel;
    private Button start;

    [SerializeField]
    private TextMeshProUGUI levelText;

    void Awake(){
        start = GetComponent<Button>();
        if(PlayerPrefs.HasKey("StageBest")){
            bestLevel = PlayerPrefs.GetInt("StageBest");
        }
        else{
           PlayerPrefs.SetInt("StageBest", 0); 
           PlayerPrefs.Save();
           bestLevel = PlayerPrefs.GetInt("StageBest");
        }

        PlayerPrefs.SetInt("ProgLevel", 0); 
        PlayerPrefs.Save();
        
        levelText.text = bestLevel.ToString();

        start.onClick.AddListener(StartLevel);
    }

    private void StartLevel(){
        SceneManager.LoadScene(2);
    }
}
