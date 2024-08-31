using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit =0.0f; //왼족 스크롤 제한
    public float rightLimit =0.0f; //오른쪽 스크롤 제한
    public float topLimit =0.0f; // 위 스크롤 제한
    public float bottomLimit =0.0f;

    public GameObject subScreen;

    public bool isForceScrollX = false;
    public float forceScrollSpeedX =0.5f;
    public bool isForceScrollY = false;
    public float forceScrollSpeedY = 0.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player !=null){
            //카메라 좌표갱신
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;

            //가로 방향 동기화
            if(isForceScrollX){
                x = transform.position.x +(forceScrollSpeedX *Time.deltaTime);
            }
            //양 끝에 이동 제한 적용
            if (x <leftLimit) x = leftLimit;
            if(x >rightLimit) x = rightLimit;

            //세로 방향 동기화
            if(isForceScrollY){
                y = transform.position.y +(forceScrollSpeedY *Time.deltaTime);
            }
            if(y <bottomLimit) y = bottomLimit;
            if(y >topLimit) y = topLimit;

            //카메라 위치의 Vector3 만들기
            Vector3 v3= new Vector3(x,y,z);
            transform.position = v3;

            // 서브 스크린 스크롤
            if (subScreen !=null){
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;
                Vector3 v = new Vector3(x/2.0f, y,z);
                subScreen.transform.position = v;
            }
        }
    }
}
