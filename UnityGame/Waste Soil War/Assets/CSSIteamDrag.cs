using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SK.KNAPSACK
{

    public class CSSIteamDrag : CSItemUI,ItemUI
    {
        Vector3 offeset = Vector3.zero;
        [SerializeField] private float DragTime = 0;
        [SerializeField] float DragTimeSpeed = 0;
        [SerializeField] GameObject CurrentSlotParent = null;
        [SerializeField] GameObject ShowMessage = null;

        private void Awake()
        {
            ItemText = GetComponentInChildren<Text>();
            ItemImage = GetComponent<Image>();
        }
        // Start is called before the first frame update
        void Start()
        {
            CurrentSlotParent = this.transform.parent.gameObject;
            ShowRenderCamera = GameObject.Find("CameraPanel");
            ShowMessage.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        }
        private void FixedUpdate()
        {
            if (CSSInventory.SharedInstance.CanDrag)
                countDown();
        }
        public void CanDrag(bool bol)
        {
            CSSInventory.SharedInstance.CanDrag = bol;
        }
        private void countDown()
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
                        CreateShowMono();
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
        public void isBeginDrag()
        {
            offeset = Input.mousePosition - GetComponent<RectTransform>().position;
            DragTime = 0;
            if (!CSSInventory.SharedInstance.CanDrag)
            {

                {
                    CSSInventory.SharedInstance.LastShowmessageBox.SetActive(false);
                    ShowMessage.SetActive(true);
                    CSSInventory.SharedInstance.LastShowmessageBox = ShowMessage;
                    ChangeShowMono();
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
                if (_t.rect.Contains(Input.mousePosition - _t.position))
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
            if (!_find)
            {
                transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }
        }



        public void SetItem(CSItemBase item, int amount = 1)
        {
            this.item = item;
            this.Amount = amount;
            ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
            this.ShowMaterial = Resources.Load<Material>(item.RenderMaterial);
            this.ShowMono = Resources.Load<GameObject>(item.ShowMonoPath);
            this.MoveMono = Resources.Load<GameObject>(item.MoveMonoPath);
        }

        public void AddAmount(int amount = 1)
        {
            Amount += amount;
            ItemText.text = Amount.ToString();
        }

        public void CreateShowMono()
        {
            GameObject _ShowMono = GameObject.Instantiate(this.ShowMono);

            ShowRenderCamera.GetComponent<Image>().enabled = true;
            ShowRenderCamera.GetComponent<Image>().material = this.ShowMaterial;
        }

        public void CreateMoveMono()
        {
            GameObject _MoveMono = GameObject.Instantiate(this.MoveMono);
            _MoveMono.GetComponent<CSPlayerBase>().SetMoveMonoID(this.item.ID);
            SceneButtonCSmananger.Instance.AddMonoToRoom(_MoveMono);
        }

        public void DeleteShowMono()
        {
            GameObject _obj=
            GameObject.FindGameObjectWithTag("Show");
            if (_obj != null)
            {
                ShowRenderCamera.GetComponent<Image>().material = null;
                ShowRenderCamera.GetComponent<Image>().enabled = false;
                Destroy(_obj);
            }
        }

        public void DeleteMoveMono()
        {
            //GameObject _obj =
            // GameObject.Find("GuaWaMone(Clone)");
            //if (_obj != null)
            //    Destroy(_obj);
            SceneButtonCSmananger.Instance.ClearRoom();
        }

        public void ChangeShowMono()
        {
            DeleteShowMono();
            CreateShowMono();
        }

        //buttoninvoke
        public void UseButtonClickDown()
        {
            if(this.item.BType!=BodyType.MainPart)
            {
                ShowMessage.SetActive(false);
                CanDrag(true);
                DeleteShowMono();
                CreateMoveMono();
            }
            else if (SceneButtonCSmananger.Instance.CountRoomMainPart())
            {
                ShowMessage.SetActive(false);
                CanDrag(true);
                DeleteShowMono();
                CreateMoveMono();
            }
            else
            {
                //ERROR.CSErrorBoxManager.Instance.SendErrorMessages("主要部分只能一个");
                //SceneButtonCSmananger.Instance.OpenCloseErrorbox(true);

                ERROR.CSErrorBoxManager.Instance.SendErrorMessages("主要部分只能一个");
                //Debug.Log(ERROR.CSErrorBoxManager.Instance);
            }
        }
        public void SellButtonClickDown()
        {

        }
        public void ExitButtonClickDown()
        {

        }
    }
}
