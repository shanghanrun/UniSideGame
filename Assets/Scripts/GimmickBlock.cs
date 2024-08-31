using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0f;
    public bool isDelete = false;

    bool isFell = false;
    float fadeTime = 0.5f;
    Rigidbody2D rbody;
    GameObject player;
    Color color;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null){
            //플레이어와의 거리 계산
            float d = Vector2.Distance(transform.position, player.transform.position);
            if(d <= length){ // 거리가 점점 작아지면서 length안에 들어옴
                if(rbody.bodyType == RigidbodyType2D.Static){
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        if(isFell){
            // 충돌하면, (isDelete일 경우) isFell true가 된다. 그러면 물체 사라지게 해야 됨
            //투명도를 변경해 페이드아웃 효과
            fadeTime -= Time.deltaTime; // 프레임과의 차이만큼 시간 차감
            color = GetComponent<SpriteRenderer>().color; // 컬러값 가져오기
            color.a = fadeTime;  // 투명도 변경
            GetComponent<SpriteRenderer>().color = color; // 컬러값 재설정

            if(fadeTime <= 0f){
                // 0보다 작으면(투명해지면) 게임오브젝트 제거
                Destroy(gameObject);
            }
        }
    }

    //접촉 판정
    void OnCollisionEnter2D(Collision2D collision){
        if(isDelete){
            isFell = true; // 낙하 플래그 true;
        }
    }
}
