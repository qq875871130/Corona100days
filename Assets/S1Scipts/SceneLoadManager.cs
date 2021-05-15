using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    static AsyncOperation m_AsyncOperation;
    static UnityAction<float> m_Progress;

    /// <summary>
    /// 加载场景
    /// </summary>
    /// 

    static public void LoadScene(string name, UnityAction<float> progress, UnityAction finish)
    {
        if (!GameObject.Find("#SceneLoadManager#"))
        {
            new GameObject("#SceneLoadManager#").AddComponent<SceneLoadManager>();
        }
        m_AsyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        m_Progress = progress;

        //加载完毕抛出事件
        m_AsyncOperation.completed += delegate (AsyncOperation obj)
        {
            finish();
            m_AsyncOperation = null;
        };

        
    }

    void Update()
    {
        if (m_AsyncOperation !=null)
        {
            //抛出加载进度
            if (m_Progress !=null)
            {
                m_Progress(m_AsyncOperation.progress);
                m_Progress = null;
            }
        }

    }


    
}
