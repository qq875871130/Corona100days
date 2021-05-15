using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class Controller : MonoBehaviour
{
    public GameObject FpsControl; 
    public Text Tips;
    public GameObject CountTips;
    public int HideTime=3;
    public float Timer = 0;//定时消息淡出
    public bool TimerStart=false ;
    //游戏开始在床上视角
    void Start()
    {
        Cursor.visible = false;
        FpsControl.GetComponent<fps>().enabled = false;
        StartCoroutine(StartGame());
    }

    void Update()
    {
        if(TimerStart )     //计时布尔为真时 计时
        { 
        Timer += Time.deltaTime;
            if (Timer > HideTime)
            {
                CountTips.GetComponent<UIAlpha>().UIHide();
                TimerStart = false;
                Timer = 0;
            }
         }
        

    }


    public void showCursor()
    {
        FpsControl .GetComponent<fps>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void hideCursor()
    {
        FpsControl .GetComponent<fps>().enabled = true ;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //自定义打印函数
    public void Log(string t)
    {
        Tips .text = t;
    }

    //自定义生命周期提示
    public void Tipmsg(string t)
    {
        CountTips.GetComponent<UIAlpha>().UIShow ();
        CountTips.GetComponentInChildren<Text>().text = t;
        TimerStart = true;
    }

    //初始协程，等3秒让黑屏淡出，按跳过键从床上起来
    private IEnumerator StartGame()
    {
        yield return new WaitUntil(BlackOver);
        Log("按 Ctrl 从床上起来");
        yield return new WaitUntil(Wakeup);
        this.GetComponent<EventManager>().ActiveCam(0);
        FpsControl.GetComponent<fps>().enabled = true;
        Log("");
    }

    bool Wakeup()
    {
        if (Input.GetKeyDown(this.GetComponent<InputManager>().ToClose)) 
        return true;
        else 
        return false;
    }

    bool BlackOver()
    {
        if (GameObject.Find("Blackmask").GetComponent<CanvasGroup>().alpha < 0.1)
            return true;
        else
            return false;
    }

}
