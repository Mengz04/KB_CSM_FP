using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuBGMManager : MonoBehaviour{
    [SerializeField] Slider volSlider;
    [SerializeField] VideoPlayer vidPlayer;
    void Awake(){
        if(!PlayerPrefs.HasKey("MenuVol")){
            PlayerPrefs.SetFloat("MenuVol", 0.15f);
            Load();
        }
        else{
            Load();
        }
    }

    public void ChangeVol(){
        vidPlayer.SetDirectAudioVolume(0, volSlider.value);
        Save();
    }

    private void Load(){
        volSlider.value = PlayerPrefs.GetFloat("MenuVol");
    }

    private void Save(){
        PlayerPrefs.SetFloat("MenuVol", volSlider.value);
    }
}
