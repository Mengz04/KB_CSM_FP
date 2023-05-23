using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAnimation : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, OnAttackPerformed;
    public void TriggerEvent(){
        OnAnimationEventTriggered?.Invoke();
    }

    public void TriggerAttack(){
        OnAttackPerformed?.Invoke();
    }
}
