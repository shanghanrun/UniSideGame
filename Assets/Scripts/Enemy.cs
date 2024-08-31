using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed =3f;
    public string direction ="left";
    public float range =0f;
    Vector3 defPos; // 시작위치
    void Start()
    {
        if(direction == "right"){
            Left(); //방향변경
        }
        //시작위치
        defPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if(range > 0f){
            if(transform.position.x < defPos.x -(range/2)){
                direction = "right";
                Left();
            }
            if(transform.position.x > defPos.x +(range/2)){
                direction = "left";
                Right();
            }
        }
    }

    void FixedUpdate(){
        //속도 갱신
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        if(direction =="right"){
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
        }else{
            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
        }
    }

    // void Turn(){
    //     // localScale을 반전시킴
    //     Vector3 localScale = transform.localScale; // 값 할당
    //     localScale.x *= -1; // x값만 반전
    //     transform.localScale = localScale;
    // }
    void Right(){
        transform.localScale = new Vector2(1,1);
    }
    void Left(){
        transform.localScale = new Vector2(-1,1);
    }

    //접촉
    private void OnTriggerEnter2D(Collider2D other) {
        if(direction =="right"){
            direction = "left";
            Left();
        } else{
            direction ="right";
            Right();
        }
    }
}
