using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIAlpha : MonoBehaviour
{
    /// <summary>
    /// 控制UI的淡入淡出效果
    /// </summary>
    /// 设定初始状态值
    public  float alpha = 0.0f;    
    //设定初始淡入主菜单的速率
    private float AlphaSpeed = 0.4f;
    //设定运行时淡入淡出速率
    public float AlphaShow = 0.0f;
    public float AlphaFade = 0.0f;
    //设定目标画布组
    private CanvasGroup cg;

    void Start()
    {
        //初始化画布组
        cg = this.gameObject .GetComponent<CanvasGroup>();
    }

    void LateUpdate()
    {

        if (alpha != cg.alpha)    //当画布组状态不等于目标状态时
        {
            //插值让画布组状态值以淡入淡出速率改变至目标状态值
            cg.alpha = Mathf.Lerp(cg.alpha, alpha, AlphaSpeed * Time.unscaledDeltaTime);
            //如果二者差绝对值小于0.01，即二者值距小于0.01，直接完成改值
            if (Mathf.Abs(alpha - cg.alpha) <= 0.02)
            {
                cg.alpha = alpha;
            }
        }


    }
    //淡入
    public void UIShow()
    {
        AlphaSpeed = AlphaShow;
        alpha = 1;
        cg.blocksRaycasts = true;
    }
    //淡出
    public void UIHide()
    {
        AlphaSpeed = AlphaFade;
        alpha = 0;
        cg.blocksRaycasts = false;
    }
}
