using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RandomEventController : MonoBehaviour
{
    //输出文字
    public Text Tip;
    //配置电视随机事件概率值
    public int[] Rate_Tv;
    //配置电视事件文字数组
    public string[] Text_Tv;
    //配置洗澡随机事件概率值
    public int[] Rate_Bath;
    //配置洗澡事件文字数组
    public string[] Text_Bath;
    //配置运动随机事件概率值
    public int[] Rate_Exercise;
    //配置运动事件文字数组
    public string[] Text_Exercise; 
    //配置网课随机事件概率值
    public int[] Rate_Class;
    //配置网课事件文字数组
    public string[] Text_Class;

    //观看完电视触发随机事件
    public void WatchRandom()
    {
        int i = RandWhich(Rate_Tv ,100);
        Tip.text = Text_Tv[i];
         switch (i)
          {
            case 0:
                {
                    this.GetComponent<StateManager>().AddLife(7);
                    this.GetComponent<StateManager>().AddHungry(-5);
                    break;
                }
            case 1:
                {
                    this.GetComponent<StateManager>().AddIntelligence(1);
                    this.GetComponent<StateManager>().AddLife(7);
                    this.GetComponent<StateManager>().AddHungry(-5);
                    break;
                }
            case 2:
                {
                    this.GetComponent<Controller>().Tipmsg("没有啥感觉");
                    break;
                }
            case 3:
                {
                    this.GetComponent<StateManager>().AddLife(-5);
                    this.GetComponent<StateManager>().AddHungry(-5);
                    break;
                }
            case 4:
                {
                    this.GetComponent<StateManager>().AddLife(10);
                    this.GetComponent<StateManager>().AddHungry(-5);
                    break;
                }
                
          }
        
    }



    //洗澡触发随机事件
    public void BathRandom()
    {
        int i = RandWhich(Rate_Bath, 100);
        Tip.text = Text_Bath[i];
        switch (i)
          {
            case 0:
                {
                    this.GetComponent<StateManager>().AddLife(8);
                    break;
                }
            case 1:
                {
                    this.GetComponent<StateManager>().AddLife(12);
                    break;
                }
            case 2:
                {
                    this.GetComponent<StateManager>().AddLife(-3);
                    break;
                }
            case 3:
                {
                    this.GetComponent<Controller>().Tipmsg("没有啥感觉");
                    break;
                }
            case 4:
                {
                    this.GetComponent<StateManager>().AddLife(10);
                    break;
                }
            case 5:
                {
                    this.GetComponent<StateManager>().AddMoney(-30);
                    break;
                }
            case 6:
                {
                    this.GetComponent<StateManager>().AddLife(-5);
                    break;
                }
            case 7:
                {
                    this.GetComponent<StateManager>().AddLife(8);
                    this.GetComponent<StateManager>().AddHungry(-5);
                    break;
                }

        }
        
    }

    //运动完触发随机事件
    public void ExerciseRandom()
    {
        int i = RandWhich(Rate_Exercise, 100);
        Tip.text = Text_Exercise[i];
        switch (i)
        {
            case 0:
                {
                    this.GetComponent<StateManager>().AddLife(10);
                    this.GetComponent<StateManager>().AddHungry(-10);
                    break;
                }
            case 1:
                {
                    this.GetComponent<StateManager>().AddLife(5);
                    this.GetComponent<StateManager>().AddHungry(-10);
                    break;   
                }
            case 2:
                {
                    this.GetComponent<StateManager>().AddLife(5);
                    this.GetComponent<StateManager>().AddHungry(-10);
                    break;
                }
            case 3:
                {
                    this.GetComponent<StateManager>().AddLife(20);
                    this.GetComponent<StateManager>().AddHungry(-10);
                    break;
                }
            case 4:
                {
                    this.GetComponent<StateManager>().AddLife(5);
                    this.GetComponent<StateManager>().AddHungry(-10);
                    break;
                }
            case 5:
                {
                    this.GetComponent<Controller>().Tipmsg("没有啥感觉");
                    break;
                }
            case 6:
                {
                    this.GetComponent<StateManager>().AddLife(20);
                    this.GetComponent<StateManager>().AddHungry(-10);
                    break;
                }
            case 7:
                {
                    this.GetComponent<StateManager>().AddHungry(-10);
                    break;
                }


        }
        
    }
    //网课随机事件
    public void ClassRandom()
    {
        int i = RandWhich(Rate_Class, 100);
        Tip.text = Text_Class[i];
       switch (i)
        {
            case 0:
                {
                    this.GetComponent<StateManager>().AddIntelligence(1);
                    break;
                }
            case 1:
                {
                    this.GetComponent<StateManager>().AddIntelligence(2);
                    this.GetComponent<StateManager>().AddLife(-8);
                    break;
                }
            case  2:
                {
                    this.GetComponent<Controller>().Tipmsg("没有啥感觉");
                    break;
                }
            case  3:
                {
                    this.GetComponent<Controller>().Tipmsg("没有啥感觉");
                    break;
                }
            case  4:
                {
                    this.GetComponent<Controller>().Tipmsg("没有啥感觉");
                    break;
                }
            case  5:
                {
                    this.GetComponent<StateManager>().AddAction(1);
                    this.GetComponent<StateManager>().AddIntelligence(1);
                    break;
                }
            case  6:
                {
                    this.GetComponent<StateManager>().AddIntelligence(2);
                    this.GetComponent<StateManager>().AddLife(-8);
                    break;
                }
            case  7:
                {
                    this.GetComponent<Controller>().Tipmsg("没有啥感觉");
                    break;
                }
            case  8:
                {
                    this.GetComponent<StateManager>().AddIntelligence(2);
                    this.GetComponent<StateManager>().AddLife(-10);
                    break;
                }
            case  9:
                {
                    this.GetComponent<StateManager>().AddIntelligence(2);
                    this.GetComponent<StateManager>().AddLife(-8);
                    break;
                }
            case  10:
                {
                    this.GetComponent<Controller>().Tipmsg("没有啥感觉");
                    break;
                }
            case 11:
                {
                    this.GetComponent<Controller>().Tipmsg("没有啥感觉");
                    break;
                }
        }
        
    }

    //概率事件函数，计算数组第几个元素发生
    public static int RandWhich(int[] rate, int total)
    {
        float RanNum = UnityEngine.Random.Range(0, total);
        int Score = 0;
        for (int i = 0; i < rate.Length; i++)
        {
            Score += rate[i];
            if (RanNum <= Score)
            {
                return i;
            }
        }
        return -1;
    }

    /* float num = RandomNum.Next(0, 100);
        if (num > &num <)
        {
        Tip.text = "";
        }
        else if (num > &num <)
        {
        Tip.text = "";
        }
    */




}
