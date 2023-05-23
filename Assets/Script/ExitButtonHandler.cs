using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ExitButtonHandler : MonoBehaviour{
    [SerializeField]
    private Button exitBtn, retryBtn;

    [SerializeField]
    private TMP_Text progLevel;

    void Awake(){
        exitBtn.onClick.AddListener(ExitAct);
        retryBtn.onClick.AddListener(RetryAct);
        progLevel.text = PlayerPrefs.GetInt("ProgLevel").ToString();
    }

    public void ExitAct(){
        PlayerPrefs.SetInt("ProgLevel", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    public void RetryAct(){
        PlayerPrefs.SetInt("ProgLevel", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(2);
    }

}
