using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject objPrefab; // 포탄 프리팹
    public float delayTime = 3f;
    public float fireSpeedX = -4f;
    public float fireSpeedY = 0f;
    public float length = 8f;

    GameObject player;
    GameObject gateObj;
    float passedTimes =0; //경과시간


    void Start()
    {
       //발사구(대포) 오브젝트 얻기
       Transform tr = transform.Find("gate");
       gateObj = tr.gameObject;
       //플레이어
       player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. Player 태그가 올바르게 설정되었는지 확인하세요.");
        } else{
            Debug.Log("player 있음");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //발사시점 판정
        passedTimes += Time.deltaTime;
        // 거리확인
        Vector2 playerPos2D = new Vector2(player.transform.position.x, 
                                            player.transform.position.y);
        if(CheckLength(playerPos2D)){
            if(passedTimes > delayTime){
                //발사!!
                passedTimes =0;
                //발사위치
                Vector3 pos = new Vector3(gateObj.transform.position.x, 
                                        gateObj.transform.position.y, 
                                        transform.position.z);

                //Prefab으로 GameObject만들기
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                //발사방향
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(fireSpeedX, fireSpeedY);
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }
    //거리확인
    bool CheckLength(Vector2 targetPos){
        bool result = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if(length >=d){
            result = true;
        }
        return result;
    }
}
