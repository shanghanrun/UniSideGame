using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;
    public float gameTime =0;
    public bool isTimeOver = false;
    public float displayTime =0; //표시 시간

    float times =0; // 현재시간

    void Start()
    {
        if(isCountDown){
            //카운트다운
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTimeOver){
            times += Time.deltaTime;
            if(isCountDown){
                // 카운트다운
                displayTime = gameTime - times;
                if(displayTime <= 0f){
                    displayTime = 0f;
                    isTimeOver = true;
                }
            } else{
                //카운트 업
                displayTime = times;
                if(displayTime >=gameTime){
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }
            Debug.Log("TIMES: "+displayTime);
        } 
    }
}
