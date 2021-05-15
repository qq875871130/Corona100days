using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UIController : MonoBehaviour
{
    //单例化
    public static UIController instance;

    public GameObject menu;

    public GameObject LoadPage;
    public Slider proSlider;
    public Text proText;

    // Start is called before the first frame update

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    #region 主菜单按钮
    //开始游戏
    public void StartGame()
    {
        LoadPage.GetComponent<UIAlpha>().UIShow();
        menu.GetComponent<UIAlpha>().UIHide();
        SceneLoadManager.LoadScene("Demo",delegate(float progress) 
        {
            proSlider.value = progress;
            proText.text = ((int)(progress*100)).ToString() + "%";
            Debug.LogFormat("加载进度:{0}", progress);
            Debug.LogFormat(proText.text);
        },
        delegate()
        {
            proSlider.value = 1.0f;
            proText.text = "加载结束！";
            Debug.Log("加载结束！");
            LoadPage.GetComponent<UIAlpha>().UIHide();
        }
        );
    }

    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
        
    }



    #endregion



}
