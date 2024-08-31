using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene2 : MonoBehaviour
{
    public string sceneName ="Stage2"; // 불러올 씬
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //씬 블러오기
    public void Load(){
        Debug.Log("버튼눌림");
        SceneManager.LoadScene(sceneName);
    }
}
