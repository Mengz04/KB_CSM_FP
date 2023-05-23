using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverZoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }
    
    public void OnPointerEnter(PointerEventData eventData){
        animator.SetBool("isHover", true);
    }

     public void OnPointerExit(PointerEventData eventData){
        animator.SetBool("isHover", false);
    }
}
