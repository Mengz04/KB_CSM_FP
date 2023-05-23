using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendorderHandler : MonoBehaviour{
    private SpriteRenderer rendOrder;
    private GameObject player;

    [SerializeField]
    private int orderFront, orderBack;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        rendOrder = GetComponent<SpriteRenderer>();
    }

    void Update(){
        RendOrder();
    }

    public void RendOrder(){
        rendOrder.sortingOrder = (player.transform.position.y > transform.position.y)? orderFront : orderBack;
    }
}
