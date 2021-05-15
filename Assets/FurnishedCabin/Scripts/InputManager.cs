using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InputManager : MonoBehaviour
{
    public  KeyCode ToSkip;      //跳过当前互动
    public KeyCode ToZoomWatch;    //看电视放大视野
    public KeyCode ToNormalWatch;   //正常观看视野
    public KeyCode ToClose;      //关闭事件对话框
    public GameObject p; //暂停菜单画布

    private bool IsStopped = false;

    void Start()
    {
    }
    void Update()
    {
        
        if (Input.GetKeyDown(ToZoomWatch))
        {
            if (GameObject.Find("WatchCamera"))      //当视角为沙发时可放大视角
            {
                this.GetComponent<EventManager>().ActiveCam(2);
            }
        }
        if (Input.GetKeyDown(ToNormalWatch ))
        {
            if (GameObject.Find("ZoomWatchCam"))      //当视角为放大时可返回视角
            {
                this.GetComponent<EventManager>().ActiveCam(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!IsStopped)
            {
                foreach (Transform child in GameObject.Find("Selection").transform)
                {
                    child.gameObject.GetComponent<UIAlpha>().UIHide();
                }
                foreach (MoveObjectController item in GameObject.FindObjectsOfType<MoveObjectController>())
                {
                    item.enabled = false;
                }

                this.GetComponent<Controller>().showCursor();
                p.GetComponent<UIAlpha>().UIShow();
                IsStopped = true;
                Time.timeScale = 0;
                
                
            }
            else
            {
                foreach (MoveObjectController item in GameObject.FindObjectsOfType<MoveObjectController>())
                {
                    item.enabled = true;
                }
                Time.timeScale = 1;
                p.GetComponent<UIAlpha>().UIHide();
                this.GetComponent<Controller>().hideCursor();
                IsStopped = false;
            }

        }
        
        

    }

    #region 暂停菜单
    public void Return()
    {
        Time.timeScale = 1;
        foreach (MoveObjectController item in GameObject.FindObjectsOfType<MoveObjectController>())
        {
            item.enabled = true;
        }
        p.GetComponent<UIAlpha>().UIHide();
        this.GetComponent<Controller>().hideCursor();
        IsStopped = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        UnityEngine.Application.Quit();
    }

    public void GoMenu()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(1);
        UIController.instance.menu.GetComponent<UIAlpha>().UIShow();
    }

    #endregion



}
