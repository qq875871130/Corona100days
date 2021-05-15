using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StateManager : MonoBehaviour
{
    //消息提示文本
    public Text StateTip;
    ///
    ///天数
    ///
    [Header("天数")]

    //天数初始值
    [Range (1,100)]
    public int Num_Day = 1;
    //天数文本
    public Text Text_Day;

    /// <summary>
    /// 金钱
    /// </summary>
    [Header("金钱")]
    //金钱初始值
    public int Num_Money = 3000;
    //金钱文本
    public Text Text_Money;



    /// <summary>
    /// 健康值
    /// </summary>
    [Header ("健康值")]
    //健康值初始值
    [Range (0,100)]
    public int Num_Life = 100;
    //健康值文本
    public Text Text_Life;
    [Header("每天受饥饿影响的健康值")]
    private int LifeByHungry_Day = 0;

    /// <summary>
    /// 饥饿值
    /// </summary>
    [Header ("饥饿值")]
    //饥饿值初始值
    [Range(0, 100)]
    public int Num_Hungry = 100;
    //饥饿值文本
    public Text Text_Hungry;

    
    
    [Header ("智慧值")]
    //智慧值初始值
    [Range(0, 100)]
    public int Num_Intelligence = 100;
    //智慧值文本
    public Text Text_Intelligence;
    //计数器计算拖延网课天数，每天+1当看网课时重新计数
    public  int Num_Delay = 0;
    [Header("每天受拖延网课天数影响的智慧值")]
    private int IntelligenceByDelay_Day = 0;


    [Header("感染率")]
    //初始化
    [Range(0, 100)]
    public int Num_Infection = 0;
    //感染率文本
    public Text Text_Infection;

    /// <summary>
    /// 行动点数
    /// </summary>
    [Header("行动点数")]
    //行动点数初始值
    [Range (0,3)]
    public int Num_Action = 3;
    //行动点数文本
    public Text Text_Action;
    //第二天行动点数"
    private int ActionByHungry_Day = 3;

    /// <summary>
    /// 出门要加的数值
    /// （口罩数，点心数，便当数，饭菜数）
    [Header("口罩数量")]
    //初始值
    public  int Num_Mask = 4;
    [Header("点心数量")]
    //初始值
    public int Num_Snack=3;
    [Header("便当数量")]
    //初始值
    public int Num_Boxmeal = 0;
    [Header("饭菜数量")]
    public int Num_Mainmeal = 0;

    bool IsOver = false;
    ///布尔值判断数值区间的激活状态
    ///作为判断区间提示是否执行了一次，防止同消息重复提示
    //健康值警告等级
    bool LifeLv1 = false;
    bool LifeLv2 = false;
    bool LifeLv3 = false;
    bool LifeLv4 = false;
    //饥饿值警告等级
    bool HungryLv1 = false;
    bool HungryLv2 = false;
    bool HungryLv3 = false;
    bool HungryLv4 = false;

    /// <summary>
    /// 动态管理
    /// </summary>



    void Update()
    {
        //将所有状态值与对应文本绑定
        Text_Day.text = Num_Day.ToString();
        Text_Money.text = Num_Money.ToString();
        Text_Life.text = Num_Life.ToString()+"/100";
        Text_Hungry.text = Num_Hungry.ToString()+"/100";
        Text_Intelligence.text = Num_Intelligence.ToString()+"/100(<color=cyan>90</color>)";
        Text_Action.text = "X" + Num_Action.ToString();
        Text_Infection.text = Num_Infection.ToString() + "%";
         
        

        //提示饥饿值状态并影响下一天健康值和行动点
        if (Num_Hungry == 0)
        {
            LifeByHungry_Day = 20;
            ActionByHungry_Day = 0;
            HungryLv1 = false;
            HungryLv2 = false;
            HungryLv3 = false;
            if (!HungryLv4)
            {
                this.GetComponent<Controller>().Tipmsg("你快饿昏了");
                HungryLv4 = true;
            }

        }
        else if (Num_Hungry > 0 & Num_Hungry < 40)
        {
            //下一天减少的健康值
            LifeByHungry_Day = 12;
            //下一天行动点
            ActionByHungry_Day = 1;
            HungryLv1 = false;
            HungryLv2 = false;
            HungryLv4 = false;
            if (!HungryLv3)
            {
                this.GetComponent<Controller>().Tipmsg("你很饿");
                HungryLv3 = true;
            }
        }
        else if (Num_Hungry >= 40 & Num_Hungry < 60)
        {
            LifeByHungry_Day = 7;
            ActionByHungry_Day = 2;
            HungryLv1 = false;
            HungryLv3 = false;
            HungryLv4 = false;
            if (!HungryLv2)
            {
                this.GetComponent<Controller>().Tipmsg("你有点饿了");
                HungryLv2 = true;
            }
        }
        else if (Num_Hungry >= 60 & Num_Hungry < 80)
        {
            LifeByHungry_Day = 3;                      
            ActionByHungry_Day = 3;
            //提示开关，其他状态初始化
            HungryLv2 = false;
            HungryLv3 = false;
            HungryLv4 = false;
            if (!HungryLv1)
            {
                this.GetComponent<Controller>().Tipmsg("你想吃点东西");
                HungryLv1 = true;
            }
        }
        else
        {   
            HungryLv1 = false;
            HungryLv2 = false;
            HungryLv3 = false;
            HungryLv4 = false;
            LifeByHungry_Day = 0;
            ActionByHungry_Day = 3;
        }
        //根据拖延网课天数改变每天影响的智慧
        if (Num_Delay < 2)
        {
            IntelligenceByDelay_Day = -2;
        }
        if (Num_Delay == 2)
        {
            IntelligenceByDelay_Day = 3;
        }
        else if (Num_Delay == 3 | Num_Delay == 4)
        {
            IntelligenceByDelay_Day = 5;
        }
        else if (Num_Delay > 5)
        {
            IntelligenceByDelay_Day = 8;
        }

        //根据健康值改变感染率状态
        if (Num_Life == 0)
        {
            this.GetComponent<Controller>().Log("<color=red>GameOver\n按ESC 重新开始</color>");
            if (!IsOver)
            {
                Time.timeScale = 0;
                IsOver = true;
            }
        }
        else if (Num_Life > 0 & Num_Life < 30)
        {
            Num_Infection = 18;
            //生命等级提示开关
            LifeLv1 = false;
            LifeLv2 = false;
            LifeLv4 = false;
            if (!LifeLv3)
            {
                this.GetComponent<Controller>().Tipmsg("感染风险高");
                LifeLv3 = true;
            }
        }
        else if (Num_Life >= 30 & Num_Life < 60)
        {
            Num_Infection = 8;
            //
            LifeLv1 = false;
            LifeLv3 = false;
            LifeLv4 = false;
            if (!LifeLv2)
            {
                this.GetComponent<Controller>().Tipmsg("你更容易感染");
                LifeLv2 = true;
            }
        }
        else if (Num_Life >= 60 & Num_Life < 85)
        {
            Num_Infection = 5;
            //
            LifeLv2 = false;
            LifeLv3 = false;
            LifeLv4 = false;
            if (!LifeLv1)
            {
                this.GetComponent<Controller>().Tipmsg("你抵抗力下降了");
                LifeLv1 = true;
            }
        }
        else if (Num_Life >= 85 & Num_Life < 100)
        {
            Num_Infection = 0;
            //
            LifeLv1 = false;
            LifeLv2 = false;
            LifeLv3 = false;
            LifeLv4 = false;
        }

        
    }

    //睡觉后清算函数
     public void DayOver()
    {
        AddDay();        //加一天，同时为网课拖延天数计数
        AddHungry(-20);    //固定消耗饥饿值
        AddLife(-LifeByHungry_Day);    //健康值更新
        AddIntelligence(-IntelligenceByDelay_Day);     //智慧值更新
        Num_Action = ActionByHungry_Day;     //行动点数更新
        //所有可互动状态更新
        this.GetComponent<EventManager>().TvWatched = false;
        this.GetComponent<EventManager>().ClassWatched = false;
        this.GetComponent<EventManager>().Bathed = false;
        this.GetComponent<EventManager>().Exercised = false;

        //提示当前拖延网课天数
        this.GetComponent<Controller>().Tipmsg("当前拖延网课" + Num_Delay.ToString() + "天");
    }

    //加一天
    public void AddDay()
    {
        Num_Day++;
        Num_Delay ++;
    }

    //加金钱
    public void AddMoney(int num)
    {
        if (Num_Money + num >= 0)
            Num_Money += num;
        else
        {
            Num_Money = 0;
            this.GetComponent<Controller>().Tipmsg("你没$了");
        }
    }

    //加健康值
    public void AddLife(int num)
    {
        if (Num_Life + num <= 100 & Num_Life + num > 0)
            Num_Life += num;
        else if (Num_Life + num > 100)
        {
            Num_Life = 100;
            this.GetComponent<Controller>().Tipmsg("你似乎健康过头了");
        }
        else
            Num_Life = 0;
    }
    //加饥饿值
    public void AddHungry(int num)
    {
        if (Num_Hungry + num <= 100 & Num_Hungry + num > 0)
            Num_Hungry += num;
        else if (Num_Hungry + num > 100)
        {
            Num_Hungry = 100;
            this.GetComponent<Controller>().Tipmsg("你吃的很撑");
        }
        else
            Num_Hungry = 0;
    }
    //加智慧值
    public void AddIntelligence(int num)
    {
        if (Num_Intelligence + num <= 100)
            Num_Intelligence += num;
        else if (Num_Intelligence + num > 100)
        {
            Num_Intelligence = 100;
            this.GetComponent<Controller>().Tipmsg("你思绪泉涌");
        }
        else
            Num_Intelligence = 0;
        this.GetComponent<Controller>().Tipmsg("你的学业一无所获");
    }
    
    //加行动点
    public void AddAction(int num)
    {
        Num_Action += num;
    }
    //提示相应状态消息

    public void TipCount(string name)
    {
        switch (name)
        {
            case "mask":
                {
                    this.GetComponent<Controller>().Tipmsg("当前口罩X"+Num_Mask.ToString ());
                    break;
                }
            case "snack":
                {
                    this.GetComponent<Controller>().Tipmsg("当前点心X" + Num_Snack.ToString());
                    break;
                }
            case "boxmeal":
                {
                    this.GetComponent<Controller>().Tipmsg("当天便当X" + Num_Boxmeal.ToString());
                    break;
                }
            case "meal":
                {
                    this.GetComponent<Controller>().Tipmsg("当天饭菜X" + Num_Mainmeal.ToString());
                    break;
                }
        }
        
    }


}
