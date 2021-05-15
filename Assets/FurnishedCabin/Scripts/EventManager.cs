using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EventManager : MonoBehaviour
{
    //引用准星画布
    public GameObject Canvas;
    //引用黑屏画布
    public GameObject BlackCanvas;
    //存放所有摄像机
    public GameObject[] cameras;
    //电视屏幕，用于播放节目或者网课
    public GameObject TV;
    //淋雨粒子系统
    public GameObject Shower;
    //滴汗粒子系统
    public GameObject SweatDrip;
    //定义现在需控制音频源
    private AudioSource SoundNow =null ;
    //引用淋浴音频源
    public AudioSource BathSound;
    //引用运动音频源
    public AudioSource SweatSound;
    //引用睡觉音频源
    public AudioSource SleepSound;
    //引用吃饭音频源
    public AudioSource EatingSound;
    //视频播放器
    VideoPlayer videoplayer;
    //设置计时器
    private float Timer;

    /// <summary>
    /// 标志位
    /// </summary>
    public  bool TvWatched = false;
    public  bool ClassWatched = false;
    public  bool Exercised = false;
    public  bool Bathed = false;


    public VideoClip[] VideoList;
    void Start()
    {
        
        videoplayer = TV.GetComponent<VideoPlayer>();
        videoplayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        
    }

    void Update()
    {

        Timer += Time.deltaTime;
        if (GameObject.Find("FPSController"))
        {
            Canvas.SetActive(true);
        }
    }
    //看电视节目
    public void OnWatchTV()
    {
        if (this.GetComponent<StateManager>().Num_Action > 0 & !TvWatched)
        {
            this.GetComponent<StateManager>().Num_Action -= 1;  ///行动点-1
            GameObject.FindWithTag("TV").GetComponent<UIAlpha>().UIHide();
            GameObject.FindWithTag("Select2").GetComponent<UIAlpha>().UIHide();
            GameObject.Find("GameManager").GetComponent<Controller>().hideCursor();     
            Canvas.SetActive(false);
            ActiveCam(1);     //转到观看视角
            videoplayer.clip = VideoList[0];
            videoplayer.SetTargetAudioSource(0, TV.GetComponent<AudioSource>());
            videoplayer.IsAudioTrackEnabled(0);
            videoplayer.Play();   //看节目
            GameObject.Find("GameManager").GetComponent<Controller>().Log("按Z放大视野\n按X沙发视角\n按F 跳过");
            StartCoroutine(Black_WatchOver());     //启动协程
            TvWatched = true;
        }
        else if (TvWatched)
            this.GetComponent<Controller>().Tipmsg("你今天的电视节目看腻了");
        else
            this.GetComponent<Controller>().Tipmsg("你精疲力竭，赶紧去睡一觉");
    }

    //吃点心
    public void OnSnack()
    {
        if (this.GetComponent<StateManager>().Num_Snack > 0)
        {
            GameObject.FindWithTag("btn3").GetComponent<UIAlpha>().UIHide();      //隐藏按钮，文字，鼠标，准星
            GameObject.FindWithTag("Select3").GetComponent<UIAlpha>().UIHide();
            GameObject.Find("GameManager").GetComponent<Controller>().hideCursor();
            Canvas.SetActive(false);
            this.GetComponent<RandomEventController>().Tip.text = "你吃完了  点心-1，饥饿值+15";
            StartCoroutine(Black_EatingOver());        //吃饭协程
            this.GetComponent<StateManager>().Num_Snack--;
            this.GetComponent<StateManager>().AddHungry(15);
        }
        else
            this.GetComponent<Controller>().Tipmsg("你没有点心了");
    }

    //吃便当
    public void OnBoxmeal()
    {
        if (this.GetComponent<StateManager>().Num_Boxmeal  > 0)
        {
            GameObject.FindWithTag("btn3").GetComponent<UIAlpha>().UIHide();      //隐藏按钮，文字，鼠标，准星
            GameObject.FindWithTag("Select3").GetComponent<UIAlpha>().UIHide();
            GameObject.Find("GameManager").GetComponent<Controller>().hideCursor();
            Canvas.SetActive(false);
            this.GetComponent<RandomEventController>().Tip.text = "你吃完了  便当-1，饥饿值+25";
            StartCoroutine(Black_EatingOver());        //吃饭协程
            this.GetComponent<StateManager>().Num_Boxmeal--;
            this.GetComponent<StateManager>().AddHungry(25);
        }
        else
            this.GetComponent<Controller>().Tipmsg("你没有便当了");
    }

    public void OnMainmeal()
    {
        if (this.GetComponent<StateManager>().Num_Mainmeal > 0)
        {
            GameObject.FindWithTag("btn3").GetComponent<UIAlpha>().UIHide();      //隐藏按钮，文字，鼠标，准星
            GameObject.FindWithTag("Select3").GetComponent<UIAlpha>().UIHide();
            GameObject.Find("GameManager").GetComponent<Controller>().hideCursor();
            Canvas.SetActive(false);
            this.GetComponent<RandomEventController>().Tip.text = "你吃完了  饭菜-1，饥饿值+40";
            StartCoroutine(Black_EatingOver());        //吃饭协程
            this.GetComponent<StateManager>().Num_Mainmeal--;
            this.GetComponent<StateManager>().AddHungry(40);
        }
        else
            this.GetComponent<Controller>().Tipmsg("你没有饭菜了");
    }



    //看网课
    public void OnClass()
    {
        if (this.GetComponent<StateManager>().Num_Action > 0 & !ClassWatched)
        {
            this.GetComponent<StateManager>().Num_Action --;   //行动点 -1
            this.GetComponent<StateManager>().Num_Delay = 0;    //重新为拖延网课天数计数
            GameObject.FindWithTag("TV").GetComponent<UIAlpha>().UIHide();
            GameObject.FindWithTag("Select2").GetComponent<UIAlpha>().UIHide();
            GameObject.Find("GameManager").GetComponent<Controller>().hideCursor();
            Canvas.SetActive(false);

            ActiveCam(1);     //转到观看视角
            videoplayer.clip = VideoList[1];
            videoplayer.SetTargetAudioSource(0, TV.GetComponent<AudioSource>());
            videoplayer.IsAudioTrackEnabled(0);
            videoplayer.Play();   //看节目
            GameObject.Find("GameManager").GetComponent<Controller>().Log("按Z放大视野\n按X沙发视角\n按F 跳过");
            StartCoroutine(Black_ClassOver());     //启动协程
            ClassWatched = true;
        }
        else if (ClassWatched)
            this.GetComponent<Controller>().Tipmsg("你课听烦了");
        else
            this.GetComponent<Controller>().Tipmsg("你精疲力竭，赶紧去睡一觉");
    }

    
    //洗澡
    public void OnBathing()
    {
        if (this.GetComponent<StateManager>().Num_Action > 0 & !Bathed)
        {
            this.GetComponent<StateManager>().Num_Action -= 1;
            ActiveCam(3);     //转到洗澡视角
            Shower.SetActive(true);                  //启动粒子系统
            BathSound.Play();
            Canvas.SetActive(false);    //关闭准星
            GameObject.Find("GameManager").GetComponent<Controller>().Log("按F 跳过");     //显示洗澡跳过提示
            StartCoroutine(Black_BathOver());    //启动黑屏对话框协程
            Bathed = true;
        }
        else if (Bathed)
            this.GetComponent<Controller>().Tipmsg("你洗的够干净了");
        else
            this.GetComponent<Controller>().Tipmsg("你精疲力竭，赶紧去睡一觉");
    }

    //运动
    public void OnExercise()
    {
        if (this.GetComponent<StateManager>().Num_Action > 0 & !Exercised)
        {
            this.GetComponent<StateManager>().Num_Action -= 1;
            ActiveCam(4);     //转到锻炼视角
            SweatDrip.SetActive(true);     //启动粒子系统
            SweatSound.Play();         //出现声音
            Canvas.SetActive(false);       //关闭准星
            GameObject.Find("GameManager").GetComponent<Controller>().Log("按F 跳过");   //显示运动跳过提示
            StartCoroutine(Black_ExerciseOver());
            Exercised = true;
        }
        else if (Exercised)
            this.GetComponent<Controller>().Tipmsg("你累了，不想再锻炼");
        else
            this.GetComponent<Controller>().Tipmsg("你精疲力竭，赶紧去睡一觉");
    }

    //睡觉
    public void OnSleep()
    {
        if (this.GetComponent<StateManager>().Num_Action == 0)
        {
            ActiveCam(5);                 //转到睡觉视角
            SleepSound.Play();        //播放声音
            Canvas.SetActive(false);         //关闭准星
            GameObject.Find("GameManager").GetComponent<Controller>().Log("按F 跳过");   //显示运动跳过提示
            StartCoroutine(Black_SleepOver());
            this.GetComponent<StateManager>().DayOver();
        }
        else
            this.GetComponent<Controller>().Tipmsg("你还不怎么困");
    }




    //摄像机转换
    public void ActiveCam(int index)
    {
        //如果摄像机组不为空
        if (cameras != null)
        {
            //禁用所有摄像机
            int i = 0;
            for (i = 0; i < cameras.Length; i++)
            {
                cameras[i].SetActive(false);
            }
            //显示所需激活的摄像机
            cameras[index].SetActive(true);
        }
    }

    //黑屏
    public void Black()
    {
        BlackCanvas.GetComponent<UIAlpha>().UIShow();
    }
    //正常屏
    public void Skip()
    {
        BlackCanvas.GetComponent<UIAlpha>().UIHide();
        ActiveCam(0);              //转回第一人称视角
        this.GetComponent<Controller>().Log("");  //清空提示消息
    }

    //洗澡事件协程
    private IEnumerator Black_BathOver()
    {
        SoundNow = BathSound;
        yield return new WaitUntil(WhenAudioOver);
        this.GetComponent<RandomEventController>().BathRandom();
        Black();
        Shower.SetActive(false);
        this.GetComponent<Controller>().Log("按 Ctrl跳过");
        Timer = 0;
        yield return new WaitUntil(IsSkip);
        Skip();
        
    }
    //电视事件协程
    private IEnumerator Black_WatchOver()
    {
        yield return   new WaitUntil (WhenWatchOver );      //等待后执行
             this.GetComponent<RandomEventController>().WatchRandom();                  //获取随机事件打印至BlackGUI
        Black();                                                  //弹出对话框并显示文字
        videoplayer.Stop();                             //关闭电视屏幕
        this.GetComponent<Controller>().Log("按 Ctrl跳过");              //提示
        Timer = 0;
        yield return new WaitUntil (IsSkip ) ;               //等待十秒后自动淡出对话框转回FPS
        Skip();
    }

    //网课事件协程
    private IEnumerator Black_ClassOver()
    {
        yield return new WaitUntil(WhenWatchOver);      //等待后执行
        this.GetComponent<RandomEventController>().ClassRandom();    //执行随机事件
        Black();                                                  //弹出对话框并显示文字
        videoplayer.Stop();                             //关闭电视屏幕
        this.GetComponent<Controller>().Log("按 Ctrl跳过");              //提示
        Timer = 0;                                                                    //计时
        yield return new WaitUntil(IsSkip);               //等待十秒后自动淡出对话框转回FPS
        Skip();
    }

    //运动事件协程
    private IEnumerator Black_ExerciseOver()
    {
        SoundNow = SweatSound;
        yield return new WaitUntil(WhenAudioOver);      //等待播放完毕或者跳过
        this.GetComponent<RandomEventController>().ExerciseRandom();     //随机事件
        Black();
        SweatDrip.SetActive(false);                  //关闭粒子系统
        this.GetComponent<Controller>().Log("按 Ctrl跳过");       //提示
        Timer = 0;
        yield return new WaitUntil(IsSkip);
        Skip();
    }
    //吃饭协程
    private IEnumerator Black_EatingOver()
    {
        ActiveCam(6);
        EatingSound.Play();
        SoundNow = EatingSound;
        this.GetComponent<Controller>().Log("按F 跳过");   //显示跳过提示
        yield return new WaitUntil(WhenAudioOver);
        Black();
        this.GetComponent<Controller>().Log("按 Ctrl跳过");       //跳过
        Timer = 0;
        yield return new WaitUntil(IsSkip);
        Skip();
    }

    //睡觉事件协程
    private IEnumerator Black_SleepOver()
    {
        SoundNow = SleepSound;
        GameObject.Find("SelectUI").GetComponent<UIAlpha>().UIHide();
        GameObject.Find("Blackmask").GetComponent<UIAlpha>().UIShow();   //隐藏UI并开始黑屏

        yield return new WaitUntil(WhenAudioOver);     //当跳过或者音频结束
        SoundNow.Stop();
        GameObject.Find("Blackmask").GetComponent<UIAlpha>().UIHide();
        Black();
        this.GetComponent<RandomEventController>().Tip.text = "你度过了这一天。";     //提示
        GameObject.Find("SelectUI").GetComponent<UIAlpha>().UIShow();
        this.GetComponent<Controller>().Log("按 Ctrl跳过");
        Timer = 0;
        yield return new WaitUntil(IsSkip);
        Skip();
    }




    //按跳过键或者视频结束时
    bool WhenWatchOver()       
    {
            if (Input.GetKeyDown(this.GetComponent<InputManager>().ToSkip) | videoplayer.length- videoplayer .time < 0.1 )     
                return true;
            else
                return false;
    }
    //按跳过键或声音结束时
    bool WhenAudioOver()
    {
        if (Input.GetKeyDown(this.GetComponent<InputManager>().ToSkip ) | SoundNow.clip.length - SoundNow.time < 0.2)
            return true;
        else
            return false;
    }

    //跳过键或计时器到10s时
    bool IsSkip()       
    {
        if (Input.GetKeyDown(this.GetComponent<InputManager>().ToClose ) |Timer > 10)
            return true;
        else
            return false;
    }

}
