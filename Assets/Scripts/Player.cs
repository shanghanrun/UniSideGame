using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rbody;
    public float speed = 3.0f; // 이동 속도
    float axisH = 0.0f; //입력

    public float jump =9f;
    public LayerMask groundLayer; // 착지할수 있는 레이어
    bool goJump = false; // 점프개시
    bool onGround = false; // 지면  초기에 onGround true로 해야 움직일 수 있다.
    // 대신 아래에서 Linecast가 잘 작동하면 true로 될 것이다.

    // 에니메이션 기본세팅
    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime ="PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime ="";
    string oldAnime ="";

    public static string gameState ="playing"; //초기에 playing이 되어야 움직임가능
    public float fallLimit = -10f; // 추락시 삭제되는 높이
    public int score =0;

    void Start()
    {
        InitializePlayer();
    }

    void InitializePlayer(){
        gameState = "playing";
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
        rbody.velocity = Vector2.zero;
        // rbody.angularVelocity = 0f;
        // rbody.isKinematic = false;
        
    }

    void Update()
    {
        if(gameState != "playing"){
            return; // "playing"이 아니면, 아래 코드 수행안함
        }

        axisH = Input.GetAxisRaw("Horizontal"); //수평방향입력 확인
        //캐릭터 방향조절
        if(axisH > 0.0f){
            //오른쪽 이동
            transform.localScale = new Vector2(1,1);
        }else if(axisH <0.0f){ // 반전
            transform.localScale = new Vector2(-1,1);
        }
        if(Input.GetButtonDown("Jump")){
            Jump();
        }
        if(transform.position.y <fallLimit){
            Destroy(gameObject);
        }
    }
    void FixedUpdate(){

        if(gameState != "playing"){
            return;
        }

        //착지판정. Linecast의 길이가 작으면 충돌감지 잘 안될 수 있다. 0.2f로 늘림
        onGround = Physics2D.Linecast(transform.position, transform.position -(transform.up *0.3f), groundLayer);

        if(onGround || axisH !=0){
            //지면위 or 속도가 0아님
            //속도 갱신하기
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        if(onGround && goJump){
            //지면 위에서 점프키 눌림
            //점프하기
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);// 순간힘 가하기
            goJump = false; // 점프 플래그 끄기
        }

        // 에니메이션 적용
        if(onGround){ // 땅에서
            if(axisH == 0){
                nowAnime = stopAnime; // 수평이동없을 때 정지
            } else{
                nowAnime = moveAnime; // 이동
            }
        } else{
            nowAnime = jumpAnime;
        }

        if(nowAnime != oldAnime){ // nowAnime가 새로운 걸로 바뀌면
            oldAnime = nowAnime;
            animator.Play(nowAnime); //에니메이션 재생
        }
    }
        
    public void Jump(){
        goJump = true;

    }

    // 접촉 감지하여 반응하는 메소드
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Goal"){
            Goal();
        } else if(other.gameObject.tag =="Dead"){
            Dead();
        }
        else if(other.gameObject.tag =="ScoreItem"){
            // 해당 item가져오기
            ItemData item = other.gameObject.GetComponent<ItemData>();// 인스턴스
            // 점수 추가하기
            score += item.value;
            // 해당 item제거
            Destroy(other.gameObject);
        }
    }

    public void Goal(){
        animator.Play(goalAnime);
        gameState = "gameclear";
        GameStop();
    }
    public void Dead(){
        animator.Play(deadAnime);

        gameState ="gameover";
        GameStop();

        // 플레이어 충돌판정 비활성화
        GetComponent<CapsuleCollider2D>().enabled = false;
        //플레이어 위로 튀어 오르게 연출
        rbody.AddForce(new Vector2(0,7), ForceMode2D.Impulse);
    }

    void GameStop(){
        //속도를 0으로 만들어 강제정지
        rbody.velocity = new Vector2(0,0);  // Vector2.zero 로 해도 된다.
    }
}
