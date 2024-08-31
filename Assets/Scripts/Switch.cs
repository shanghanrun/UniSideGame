using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject targetMoveBlock;
    public Sprite imageOn;
    public Sprite imageOff;
    public bool on = false; // 스위치상태
    void Start()
    {
        if(on){
            GetComponent<SpriteRenderer>().sprite = imageOn;
        }else{
            GetComponent<SpriteRenderer>().sprite = imageOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //접촉 감지
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag =="Player"){
            if(on){
                on = false;
                GetComponent<SpriteRenderer>().sprite = imageOff;
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Stop();
            } else{
                on = true;
                GetComponent<SpriteRenderer>().sprite = imageOn;
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Move();
            }
        }
    }
}
