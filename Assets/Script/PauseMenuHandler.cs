using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PauseMenuHandler : MonoBehaviour{
    [SerializeField]
    private GameObject PMH;
    [SerializeField]
    private Button resumeBtn, exitBtn, retryBtn;

    [SerializeField]
    private Player player;

    void Awake(){
        PMH.SetActive(false);
        exitBtn.onClick.AddListener(ExitAct);
        resumeBtn.onClick.AddListener(ResumeAct);
        retryBtn.onClick.AddListener(RetryAct);
    }

    public void ExitAct(){
        SceneManager.LoadSceneAsync(1);
        PlayerPrefs.SetInt("ProgLevel", 0);
        PlayerPrefs.Save();
    }

    public void ResumeAct(){
        player.DePause();
        PMH.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RetryAct(){
        PlayerPrefs.SetInt("ProgLevel", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

}
