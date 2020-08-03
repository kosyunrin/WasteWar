using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SK.KNAPSACK;

public class SceneButtonCSmananger :MonoBehaviour
{
    public static SceneButtonCSmananger Instance=null;
    private GameObject _camera = null;
    private List<GameObject> ClearRoomList;
    public bool CanMoveOtherMono=false;
    
    private void Awake()
    {
        _camera=
        GameObject.Find("Main Camera");
        if(Instance==null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        ClearRoomList = new List<GameObject>();
    }
    public void isActionMode()
    {
        var _cscameraController = _camera.GetComponent<CSCameraController>().enabled = !_camera.GetComponent<CSCameraController>().enabled;
    }
    public void ClearRoom()
    {

        for (int i = ClearRoomList.Count - 1; i >= 0; i--)
        {
            if (ClearRoomList[i] != null)
            {
                Destroy(ClearRoomList[i]);
                ClearRoomList.Remove(ClearRoomList[i]);
            }
        }

    }
    public bool CountRoomMainPart()
    {
        int count = 0;
        for (int i = ClearRoomList.Count - 1; i >= 0; i--)
        {
            var _Movemono = ClearRoomList[i];
            if (_Movemono != null)
            {
                    CSItemBase item = CSSInventory.SharedInstance.GetItem(_Movemono.GetComponent<CSPlayerBase>().GetMoveMonoID());
                    if (item != null)
                    {
                    Debug.Log(item.BType);

                    if (item.BType == BodyType.MainPart)
                        {

                        count++;
                        }
                    }
            }
            if (count > 0) return false;
        }
        return true;
    }
    public void ClearShowMono()
    {
        GameObject _obj =
            GameObject.FindGameObjectWithTag("Show");
        if (_obj != null)
        {
            var
            ShowRenderCamera = GameObject.Find("CameraPanel");
            ShowRenderCamera.GetComponent<Image>().material = null;
            ShowRenderCamera.GetComponent<Image>().enabled = false;
            Destroy(_obj);
        }
    }
    public void AddMonoToRoom(GameObject mono)
    {
        ClearRoomList.Add(mono);
    }
    public void  SetCanDrogitems(bool bo)
    {
        CSSInventory.SharedInstance.CanDrag = bo;
    }
    public void WareRoomExit()
    {
        var _Insventory = GameObject.Find("Inventory");
        if(_Insventory!=null)
        {
            CloseItemMessagebox();
            _Insventory.SetActive(false);
        }
    }
    public void CloseItemMessagebox()
    {
        GameObject _g =
        GameObject.Find("ShowMessage");
        if(_g!=null)
        {
            _g.SetActive(false);
        }
    }
  
}
