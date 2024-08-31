using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX =0f;
    public float moveY =0f;
    public float times =0f; // 시간
    public float wait =0f; // 정지시간  waitTime 대신에 잘 사용
    public bool isMoveWhenOn = false;

    public bool isCanMove = true;
    float perDX;  //프레임당 x 이동값
    float perDY;
    Vector3 defPos;  // 초기위치
    bool isReverse = false; // 반전여부
    void Start()
    {
        // 초기위치
        defPos = transform.position;
        // 1프레임에 이동하는 시간
        float timeStep = Time.fixedDeltaTime;
        // 1프레임 x,y이동값
        perDX = moveX / (1.0f /timeStep *times);
        perDY = moveY / (1.0f /timeStep *times);

        if(isMoveWhenOn){
            //처음에는 움직이지 않고, 올라가면 움직이기 시작
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isCanMove){
            //이동중
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX =false;  // x방향 이동 종료
            bool endY =false;

            if(isReverse){
                //반대방향 이동
                // 이동량이 양수고, 이동위치가 초기 위치보다 적거나
                // 이동량이 음수고, 이동위치가 초기 위치보다 큰 경우
                if((perDX >=0f && x<= defPos.x) || (perDX < 0f && x >= defPos.x)){
                    endX = true; // X방향 이동 종료
                }
                if((perDY >=0f && y<= defPos.y) || (perDY < 0f && y >= defPos.y)){
                    endY = true; // X방향 이동 종료
                }

                //블록이동
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));
            } else{
                //정방향 이동
                // 이동량이 양수고, 이동위치가 초기위치보다 크거나
                // 이동량이 음수고, 이둥위치가 초기+이동거리 보다 작은 경우
                if((perDX >= 0f && x >= defPos.x +moveX) || (perDX < 0f && x<= defPos.x + moveX)){
                    endX = true;
                }
                if((perDY >= 0f && y >= defPos.y +moveY) || (perDY < 0f && y<= defPos.y + moveY)){
                    endY = true;
                }
                //블록이동
                Vector3 v = new Vector3(perDX, perDY, defPos.z);
                transform.Translate(v);
            }

            if(endX && endY){
                //이동 종료
                if(isReverse){
                    //위치가 어긋나는 것을 방지하고, 정면방향 이동으로 돌아오기 전에 초기 위치로 돌리기
                    transform.position = defPos;
                }
                isReverse = !isReverse;
                isCanMove = false;
                if(isMoveWhenOn == false){
                    // 올라갔을 때 움직이는 값이 꺼진 경우
                    Invoke("Move", wait);
                }
            }
        }
    }
    public void Move(){
        isCanMove = true;
    }

    // 이동하지 못하게 만들기
    public void Stop(){
        isCanMove = false;
    }

    //접촉 판정
    private void OnCollisionEnter2D(Collision2D collision) { //collision은 other
        if(collision.gameObject.tag == "Player"){
            collision.transform.SetParent(transform); //플레이어를 이동블럭의 자식으로 만든다.  collision(player)의 부모를 transform(Block)으로 한다.
            if(isMoveWhenOn){
                // 올라갔을 때, 움직이는 경우면
                isCanMove = true; // 이동하게 만들기
            }
        }
    }

    // 접촉종료
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            collision.transform.SetParent(null); // 부모가 없게 만든다.
        }
    }
}
