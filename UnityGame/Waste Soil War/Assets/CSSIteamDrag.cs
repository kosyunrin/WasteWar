using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSSIteamDrag : MonoBehaviour
{
    Vector3 offeset=Vector3.zero;
    [SerializeField] private float DragTime=0;
    [SerializeField] float DragTimeSpeed=0;
    [SerializeField] GameObject CurrentSlotParent = null;
    [SerializeField] GameObject ShowMessage = null;
    // Start is called before the first frame update
    void Start()
    {
        CurrentSlotParent = this.transform.parent.gameObject;
        ShowMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(CSSInventory.SharedInstance.CanDrag)
        countDown();
    }
    public void CanDrag(bool bol)
    {
        CSSInventory.SharedInstance.CanDrag = bol;
    }
    private void  countDown()
    {
        if (Input.GetMouseButton(0))
        {
            var _t = CurrentSlotParent.GetComponent<RectTransform>();
            if (_t.rect.Contains(Input.mousePosition - _t.position))
            {
                DragTime += Time.deltaTime * DragTimeSpeed;
            }
            else
            {
                DragTime = 0;
                ShowMessage.SetActive(false);
            }
            if (DragTime > 2.0f)
            {
                if (ShowMessage != null)
                {
                    ShowMessage.SetActive(true);
                    CSSInventory.SharedInstance.LastShowmessageBox = ShowMessage;
                    transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    CSSInventory.SharedInstance.CanDrag = false;
                }
            }
        }
        else
        {
            DragTime = 0;
        }
    }
    public  void isBeginDrag()
    {

        offeset = Input.mousePosition - GetComponent<RectTransform>().position;
        DragTime = 0;
        if (!CSSInventory.SharedInstance.CanDrag)
        {

            {
                CSSInventory.SharedInstance.LastShowmessageBox.SetActive(false);
                ShowMessage.SetActive(true);
                CSSInventory.SharedInstance.LastShowmessageBox = ShowMessage;
            }
        }

    }
    public void isDuringDrag()
    {
        if (!CSSInventory.SharedInstance.CanDrag)
        {
           
                return;
        }

        GetComponent<RectTransform>().position = Input.mousePosition - offeset;
    }
    public void isEndDrag()
    {
        if (!CSSInventory.SharedInstance.CanDrag) return;

        GameObject _inventory = CSSInventory.SharedInstance.gameObject;
        bool _find = false;
        for (int i = 0; i < _inventory.transform.childCount; i++)
        {
            RectTransform _t = _inventory.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
            if(_t.rect.Contains(Input.mousePosition-_t.position))
            {
                //GameObject _tmono = _inventory.transform.GetChild(i).gameObject;
                //_tmono.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
                //_tmono.SetActive(true);
                //GetComponent<Image>().sprite = null;
                //this.gameObject.SetActive(false);
                transform.SetParent(_t);
                transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
                _find = true;
                CurrentSlotParent = _t.gameObject;
                break;
            }
        }
        if(!_find)
        {
            transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }
}
