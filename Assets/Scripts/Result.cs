using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public GameObject scoreText;
    void Start()
    {
        scoreText.GetComponent<Text>().text = GameManager.totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
