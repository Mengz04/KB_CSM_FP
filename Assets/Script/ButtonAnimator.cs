using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    private Animator animator;
    private Button button;
    private TextMeshProUGUI buttonText;

    private Color red;
    
    void Awake(){
        animator = GetComponent<Animator>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        red = new Color(0.93f, 0.35f, 0.35f);
    }

    public void OnPointerEnter(PointerEventData eventData){
        animator.SetBool("isHover", true);
        buttonText.color = red;
    }

     public void OnPointerExit(PointerEventData eventData){
        animator.SetBool("isHover", false);
        buttonText.color = Color.white;
    }
}
