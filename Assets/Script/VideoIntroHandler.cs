using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.InputSystem;
 
public class VideoIntroHandler : MonoBehaviour{
    
    [SerializeField]
    private InputActionReference skip;

    VideoPlayer video;

    private void OnEnable(){
        skip.action.performed += SkipIntro;
    }

    private void OnDisable(){
        skip.action.performed -= SkipIntro;
    }
    
    private void SkipIntro(InputAction.CallbackContext obj){
        SceneManager.LoadScene(1);
    }
    void Start(){
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += LoadScene;
    }
 
     void LoadScene(VideoPlayer vp){
        SceneManager.LoadScene(1);
    }
}