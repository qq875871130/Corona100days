using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class MoveObjectController : MonoBehaviour 
{
	public float reachRange = 1.8f;
	private Animator anim;
	private Camera fpsCam;
	private GameObject player;

	private const string animBoolName = "isOpen_Obj_";

	private bool playerEntered;
	private bool showInteractMsg;
    private bool showTipmsg;
	private GUIStyle guiStyle;
    private GUIStyle interactGUIStyle;

    private string msg;
    private string intermsg;
	private int rayLayerMask; 


	void Start()
	{
        //Initialize moveDrawController if script is enabled.
        player = GameObject.FindGameObjectWithTag("Player");

		fpsCam = Camera.main;
		if (fpsCam == null)	//a reference to Camera is required for rayasts
		{
			Debug.LogError("A camera tagged 'MainCamera' is missing.");
		}
        
		//create AnimatorOverrideController to re-use animationController for sliding draws.
		anim = GetComponent<Animator>(); 
        if(anim !=null )
		anim.enabled = false;  //disable animation states by default.  

		//the layer used to mask raycast for interactable objects only
		LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
		rayLayerMask = 1 << iRayLM.value;  

		//setup GUI style settings for user prompts
		setupGui();

	}
		
	void OnTriggerEnter(Collider other)
	{		
		if (other.gameObject == player)		//player has collided with trigger
		{			
			playerEntered = true;

		}
	}

	void OnTriggerExit(Collider other)
	{		
		if (other.gameObject == player)		//player has exited trigger
		{			
			playerEntered = false;
			//hide interact message as player may not have been looking at object when they left
			showInteractMsg = false;
            showTipmsg = false;	
		}
	}



	void Update()
	{
        if (GameObject.Find("FPSController"))
        {
            if (playerEntered)
            {
                //定义摄像头的世界坐标圆点为射线原点
                Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;

                //如果射线投射到了碰撞体
                if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, reachRange, rayLayerMask))
                {
                    MoveableObject moveableObject = null;
                    ObjectInfo objname = this.gameObject.GetComponentInChildren<ObjectInfo>();
                    //is the object of the collider player is looking at the same as me?
                    if (objname == true)       //需互动的物体挂载了ObjectInfo类
                    {
                        showTipmsg = true;              //开启提示
                        string name = objname.ReturnName();   //可互动的物体名
                        intermsg = "按E " + name;
                        if (Input.GetKeyDown(KeyCode.E))         //按E对不同指向物体执行相应操作
                        {
                            ///互动物件的执行函数
                            switch (name)
                            {
                                case "使用电视":
                                    {
                                        GameObject.Find("GameManager").GetComponent<Controller>().showCursor();
                                        GameObject.FindWithTag("Select2").GetComponent<UIAlpha>().UIShow();
                                        GameObject.FindWithTag ("TV").GetComponent<UIAlpha>().UIShow();
                                    }
                                    break;
                                case "开始运动":
                                    {
                                        GameObject.Find("GameManager").GetComponent<EventManager>().OnExercise();
                                        break;
                                    }
                                case "出门":
                                    {
                                        GameObject.Find("GameManager").GetComponent<Controller>().Tipmsg("敬请期待");
                                        break;
                                    }
                                case "洗澡":
                                    {
                                        GameObject.Find("GameManager").GetComponent<EventManager>().OnBathing();
                                        break;
                                    }
                                case "吃饭":
                                    {
                                        GameObject.Find("GameManager").GetComponent<Controller>().showCursor();
                                        GameObject.FindWithTag("Select3").GetComponent<UIAlpha>().UIShow();
                                        GameObject.FindWithTag("btn3").GetComponent<UIAlpha>().UIShow();
                                        break;
                                    }
                                case "睡觉":
                                    {
                                        GameObject.Find("GameManager").GetComponent<EventManager>().OnSleep();
                                        break;
                                    }
                                case "检查口罩":
                                    {
                                        GameObject.Find("GameManager").GetComponent<StateManager>().TipCount("mask");
                                        break;
                                    }
                                case "检查点心":
                                    {
                                        GameObject.Find("GameManager").GetComponent<StateManager>().TipCount("snack");
                                        break;
                                    }
                                case "检查便当":
                                    {
                                        GameObject.Find("GameManager").GetComponent<StateManager>().TipCount("boxmeal");
                                        break;
                                    }
                                case "检查饭菜":
                                    {
                                        GameObject.Find("GameManager").GetComponent<StateManager>().TipCount("meal");
                                        break;
                                    }

                            }
                        }
                    }
                   
                    
                    if (!isEqualToParent(hit.collider, out moveableObject))
                    {   //it's not so return;
                        return;
                    }

                    if (moveableObject != null)     //hit object must have MoveableDraw script attached
                    {
                        showInteractMsg = true;
                        string animBoolNameNum = animBoolName + moveableObject.objectNumber.ToString();

                        bool isOpen = anim.GetBool(animBoolNameNum);    //need current state for message.
                        msg = getGuiMsg(isOpen);

                        if (Input.GetButtonDown("Fire1"))
                        {
                            anim.enabled = true;
                            anim.SetBool(animBoolNameNum, !isOpen);
                            msg = getGuiMsg(!isOpen);
                        }

                    }
                }
                
                    
            }
            
        }
                else     //未触发碰撞器时
        {
            showInteractMsg = false;
                    showTipmsg = false;
                 
        }
            
        
	}

	//is current gameObject equal to the gameObject of other.  check its parents
	private bool isEqualToParent(Collider other, out MoveableObject draw)
	{
		draw = null;
		bool rtnVal = false;
		try
		{
			int maxWalk = 6;
			draw = other.GetComponent<MoveableObject>();

			GameObject currentGO = other.gameObject;
			for(int i=0;i<maxWalk;i++)
			{
				if (currentGO.Equals(this.gameObject))
				{
					rtnVal = true;	
					if (draw== null) draw = currentGO.GetComponentInParent<MoveableObject>();
					break;			//exit loop early.
				}

				//not equal to if reached this far in loop. move to parent if exists.
				if (currentGO.transform.parent != null)		//is there a parent
				{
					currentGO = currentGO.transform.parent.gameObject;
				}
			}
		} 
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
		}
			
		return rtnVal;

	}
		

	#region GUI Config

	//configure the style of the GUI
	private void setupGui()
	{
        //设置开关门窗GUI风格
		guiStyle = new GUIStyle();
		guiStyle.fontSize = 20;
		guiStyle.fontStyle = FontStyle.Bold;
		guiStyle.normal.textColor = Color.white;
		msg = "Press Fire1 to Open";
        //设置互动GUI风格
        interactGUIStyle = new GUIStyle();
        interactGUIStyle.fontSize = 24;
        interactGUIStyle.fontStyle = FontStyle.Bold;
        interactGUIStyle.normal.textColor = new Color(0f, 200f, 255f, 255f);
        intermsg  = "按E 互动";
        
	}

	private string getGuiMsg(bool isOpen)
	{
		string rtnVal;
		if (isOpen)
		{
			rtnVal = "点击 关闭";
		}else
		{
			rtnVal = "点击 打开";
		}

		return rtnVal;
	}



	void OnGUI()
	{
		if (showInteractMsg)  //show on-screen prompts to user for guide.
		{
			GUI.Label(new Rect (50,Screen.height - 50,200,50), msg,guiStyle);
		}
        
        else if (showTipmsg)  //显示物品互动提示
        {
            //在准星附近绘制提示文字
            GUI.Label(new Rect(Screen.width / 2 + 10, Screen.height / 2 + 10, 200, 50), intermsg, interactGUIStyle);
        }
       
    }
    //End of GUI Config --------------
    #endregion



}
