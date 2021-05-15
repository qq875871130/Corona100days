using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class movietexture : MonoBehaviour
{
    public VideoPlayer movie;
    public GameObject menu;
    public GameObject videomenu;
    bool CanSkip = true;

    public static movietexture instance;
    void Start()
    {
        instance = this;
        movie.Play(); 
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) | movie.length - movie.time < 0.1)
        {
            if (CanSkip)
            {
                SkipVideo();
            }
        }
    }



    //播放介绍视频
    public void PlayVideo()
    {
        CanSkip = true;
        movie.Play();
        videomenu.SetActive(true);
        menu.GetComponent<UIAlpha>().UIHide();
    }

    //跳过介绍视频
    public void SkipVideo()
    {
        movie.Stop();
        videomenu.SetActive(false);
        menu.GetComponent<UIAlpha>().UIShow();
        CanSkip = false;
    }




}
