using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace SK.TOUCH
{
    public class CSTouch : MonoBehaviour
    {
        public static CSTouch Inst;
        private void Awake()
        {
            Inst = this;
        }
        public bool tuoka;
        private Transform model = null;
        public UnityEvent DoubleClickEnvents=null;
        private double t1;
        private double t2;  
        private void Start()
        {
            tuoka = true;
            model = null;
        }
        private Touch oldTouch1;  //上次触摸点1(手指1)  
        private Touch oldTouch2;  //上次触摸点2(手指2)  
        void Update()
        {
            onDoubleClick();
            if (tuoka && model != null)
            {
                //没有触摸  
                if (Input.touchCount <= 0)
                {
                    return;
                }

                //单点触摸， 水平上下旋转
                if (1 == Input.touchCount)
                {
                    Touch touch = Input.GetTouch(0);
                    Touch myTouch = Input.touches[0];
                    Vector2 deltaPos = touch.deltaPosition;
                    Vector2 startPos = touch.rawPosition;
                    Vector2 lastPos = deltaPos + startPos;


                    Debug.Log(Vector2.Angle(deltaPos, Vector3.down));

                    if (Vector2.Angle(deltaPos, Vector3.down) >= 60)
                    {
                        if (Vector2.Angle(deltaPos, Vector3.down) <= 100)
                        {
                            model.Rotate(Vector3.down * deltaPos.x * 0.5f, Space.World);
                        }
                        else
                        {
                            model.Rotate(Vector3.right * deltaPos.y * 0.5f, Space.World);
                        }
                    }
                    else if (Vector2.Angle(deltaPos, Vector3.right) >= 60)
                    {
                        model.Rotate(Vector3.right * deltaPos.y * 0.5f, Space.World);
                    }
                    //Debug.Log(VectorAngle(deltaPos, Vector3.right));
                }
                if (2 == Input.touchCount)
                {
                    //多点触摸, 放大缩小  
                    Touch newTouch1 = Input.GetTouch(0);
                    Touch newTouch2 = Input.GetTouch(1);

                    //第2点刚开始接触屏幕, 只记录，不做处理  
                    if (newTouch2.phase == TouchPhase.Began)
                    {
                        oldTouch2 = newTouch2;
                        oldTouch1 = newTouch1;
                        return;
                    }

                    //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
                    float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
                    float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

                    //两个距离之差，为正表示放大手势， 为负表示缩小手势  
                    float offset = newDistance - oldDistance;

                    //放大因子， 一个像素按 0.01倍来算(100可调整)  
                    float scaleFactor = offset / 100f;
                    Vector3 localScale = model.localScale;
                    Vector3 scale = new Vector3(localScale.x + scaleFactor,
                                                localScale.y + scaleFactor,
                                                localScale.z + scaleFactor);

                    //最小缩放到 0.3 倍 ，最大放大到 3 倍
                    if (scale.x > 0.3f && scale.y > 0.3f && scale.z > 0.3f && scale.x < 3f && scale.y < 3f && scale.z < 3f)
                    {
                        model.localScale = scale;
                    }
                    //记住最新的触摸点，下次使用  
                    oldTouch1 = newTouch1;
                    oldTouch2 = newTouch2;
                }
            }
            else if (model == null)
            {

            }
        }
        public void ReSetModel()
        {
            model.localEulerAngles = Vector3.zero;
            model.localPosition = Vector3.zero;
            model.localScale = Vector3.one * 1f;
        }
        float VectorAngle(Vector2 from, Vector2 to)
        {
            float angle;
            Vector3 cross = Vector3.Cross(from, to);
            angle = Vector2.Angle(from, to);
            return cross.z > 0 ? -angle : angle;
        }
        public void SetTarGet(Transform target)
        {
            model = target;
        }
        public GameObject Get3DTouchTarget(Touch touch)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("xxx");
                Ray ray = Camera.main.ScreenPointToRay(touch.position);//从摄像机发出到点击坐标的射线
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    GameObject gameObj = hitInfo.collider.gameObject;
                    if (gameObj.tag == "move")//当射线碰撞目标为boot类型的物品 ，执行拾取操作
                    {
                        return gameObj;
                    }
                }
            }
            return null;

        }

        public GameObject Get3DMouseTarget()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线
                //RaycastHit hitInfo;
                //if (Physics.Raycast(ray, out hitInfo))
                //{
                //    // print("null "+ hitInfo.collider.name);
                //    GameObject gameObj = hitInfo.collider.gameObject;
                //    if (gameObj.tag == "Move")//
                //    {
                //        //print("null " + hitInfo.collider.name);
                //        t2 = Time.realtimeSinceStartup;
                //        if (t2 - t1 < 0.2)
                //        {
                //            print("double click " + gameObj.name);
                //            t2 = 0;
                //        }
                //        t1 = t2;
                //        return gameObj;
                //    }
                //}

                t2 = Time.realtimeSinceStartup;
                if (t2 - t1 < 0.2)
                {
                    print("null");
                    DoubleClickEnvents.Invoke();
                    t2 = 0;
                }
                t1 = t2;
            }
            return null;

        }

        public void  DoubleClickAddEnvets(UnityAction mAction)
        {
            DoubleClickEnvents.AddListener(mAction);
        }
        void onDoubleClick()
        {
#if UNITY_STANDALONE_WIN

            if (Input.GetMouseButtonDown(0))
            {
                t2 = Time.realtimeSinceStartup;
                if (t2 - t1 < 0.2)
                {
                    print("null");
                    DoubleClickEnvents.Invoke();
                    t2 = 0;
                }
                t1 = t2;
            }

#elif UNITY_ANDROID || UNITY_IPHONE
            
if (Input.GetMouseButtonDown(0))
            {
                t2 = Time.realtimeSinceStartup;
                if (t2 - t1 < 0.2)
                {
                    print("null");
                    DoubleClickEnvents.Invoke();
                    t2 = 0;
                }
                t1 = t2;
            }
 
#endif
        }
    }
}

