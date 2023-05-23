using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour{

    private WeaponParent weaponParent;
    private bool isDead = false;
    private PlayerMover playerMover;
    private Vector2 pointerInput, movementInput;
    public Vector2 PointerInput => pointerInput;
    public Animator animator {get; set;}

    [SerializeField]
    private GameObject statHUD;

    [SerializeField]
    private GameObject bgm;

    [SerializeField]
    private GameObject PMH;

    private int level=0, exp=0, maxExp=50;

    private bool isPause = false;

    public void DePause(){
        isPause = false;
    }
    public void PlayerSetDead(){
        Destroy(bgm);
        isDead = true;
        PlayerPrefs.SetInt("ProgLevel", level);
        PlayerPrefs.SetInt("StageBest", (PlayerPrefs.GetInt("StageBest")>PlayerPrefs.GetInt("ProgLevel")) ? PlayerPrefs.GetInt("StageBest") : PlayerPrefs.GetInt("ProgLevel"));
        PlayerPrefs.Save();
        SceneManager.LoadScene(3);
    }

    public int GetExp(){return exp;}
    public int GetLevel(){return level;}
    public int GetMaxExp(){return maxExp;}

    [SerializeField]
    private InputActionReference movement, normalAttack, pointerPosition, menu;

    private void OnEnable(){
        if(isDead == false) normalAttack.action.performed += PerformAttack;
        if(isDead == false) menu.action.performed += Pause;
    }

    private void OnDisable(){
        if(isDead == false) normalAttack.action.performed -= PerformAttack;
    }

    public void Pause(InputAction.CallbackContext context){
        if(isPause == true){return;}
        else{
            isPause = true;
            PMH.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
    private void PerformAttack(InputAction.CallbackContext obj){
        if(weaponParent == null){
            Debug.LogError("Weapon parent is null", gameObject);
            return;
        }
        weaponParent.Attack();
    }

    private void Awake(){
        PMH.SetActive(false);
        gameObject.tag = "Player";
        animator = GetComponent<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        playerMover = GetComponent<PlayerMover>();
        statHUD.SetActive(false);
    }
    void Update(){
        pointerInput = GetPointerInput();
        movementInput = movement.action.ReadValue<Vector2>();
        playerMover.MovementInput = movementInput;
        weaponParent.pointerPosition = pointerInput;
    }

    public void AddExp(int val){
        if(isDead == true) return;
        exp += val;
        if(exp >= maxExp){
            level++;
            exp = 0;
            maxExp +=25;
        }
    }

    private Vector2 GetPointerInput(){
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void Tracked(){
        statHUD.SetActive(true);
    }
    public void Untracked(){
        statHUD.SetActive(false);
    }
}
