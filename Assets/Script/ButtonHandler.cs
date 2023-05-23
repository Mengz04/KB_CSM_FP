using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour{
    [SerializeField]
    private Button playBtn, optionsBtn, quitBtn;

    [SerializeField]
    private Animator playSM, optionsSM;

    void Start(){
        Time.timeScale = 1;
        playBtn.onClick.AddListener(PlaySM);
        optionsBtn.onClick.AddListener(OptionsSM);
        quitBtn.onClick.AddListener(QuitSM);
    }

    public void PlaySM(){
        if(playSM.GetBool("isShown")){playSM.SetBool("isShown", false);}
        else{
            optionsSM.SetBool("isShown", false);
            AnimCoroutine();
            playSM.SetBool("isShown", true);
        }
    }

    public void OptionsSM(){
        if(optionsSM.GetBool("isShown")){optionsSM.SetBool("isShown", false);}
        else{
            playSM.SetBool("isShown", false);
            AnimCoroutine();
            optionsSM.SetBool("isShown", true);
        }
    }

    public void QuitSM(){
        Application.Quit();
    }

    IEnumerator AnimCoroutine(){
        yield return new WaitForSeconds(0.25f);
    }
}
