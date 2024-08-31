
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr; // Dead
    public Sprite gameClearSpr; //Goal
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;

    Image titleImage;
    // 시간제한 추가 
    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeController;

    //점수 추가
    public GameObject scoreText;
    public static int totalScore; //점수 총합
    public int stageScore =0; //스테이지 점수

    void Start()
    {
        // 1초후에 InactiveImage 를 실행하는데, 
        // 아래에 정의되어 있다. 'GAME START' 이지미를 없애는 것이다.
        // 그러면, 처음 'GAME START'가 보이다가, 1초후에 없어진다.
        Invoke("InactiveImage", 1.0f);
        // 버튼(패널) 숨기기
        panel.SetActive(false);

        // 시간제한 추가
        timeController = GetComponent<TimeController>();
        if(timeController !=null){
            if(timeController.gameTime == 0f){
                timeBar.SetActive(false); // 시간이 0이면 숨긴다.
            }
        }

        //점수추가
        UpdateScore(); // 화면용 점수 갱신.
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.gameState == "gameclear"){
            // 게임 클리어되었을 때

            // 새로운 게임플레이하도록 하는 화면
            mainImage.SetActive(true); //이미지 표시
            panel.SetActive(true); // 버튼(패널)표시

            // RESTART버튼 무효화
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            Player.gameState = "gameend";

            // 시간제한 추가
            if(timeController != null){
                timeController.isTimeOver = true;  // 시간 카운트 중지
                // 점수추가 (남은시간 자체가 점수가 된다.)
                int time = (int)timeController.displayTime;
                totalScore += time *10;  // 남은 시간에 곱해서 더한다.
            }
            // 점수추가
            totalScore += stageScore;
            stageScore =0;  // 지금 게임클리어로 게임이 끝난상황
            UpdateScore(); // 화면용 점수 갱신
        } else if(Player.gameState == "gameover"){ // dead
            //게임오버되었을 때

            mainImage.SetActive(true);
            panel.SetActive(true);
            //Next 버튼 비활성화
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            Player.gameState = "gameend";

            // 시간제한
            if(timeController !=null){
                timeController.isTimeOver = true;
            }
        } else if(Player.gameState == "playing"){
            //게임중
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player playerInstance = player.GetComponent<Player>();

            // 화면에 시간 시간 갱신 반영
            if(timeController !=null){
                int time = (int)timeController.displayTime; // 소수점이하 버리기
                //시간 갱신
                timeText.GetComponent<Text>().text = time.ToString();
                //타임오버
                if(time ==0){
                    playerInstance.Dead(); //나는 GameOver대신에 Dead로 바꾸었다.
                }
            }
            if(playerInstance.score !=0){
                stageScore += playerInstance.score;
                playerInstance.score =0;
                UpdateScore();
            }
        }
    }
    // 이미지 숨기는 메소드
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    void UpdateScore(){ // 화면용 점수갱신
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
