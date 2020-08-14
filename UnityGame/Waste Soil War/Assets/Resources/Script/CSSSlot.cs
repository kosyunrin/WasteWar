using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SK
{

    public class CSSSlot : MonoBehaviour
    {
        [SerializeField] GameObject ItemPrefas = null;
        //private int Amount;
        private void Awake()
        {
            if (ItemPrefas == null)
                ItemPrefas = Resources.Load<GameObject>("prefabs/item");
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void PutInside(CSItemBase item)
        {
            if (transform.childCount == 0)
            {
                GameObject _itemPrefas = GameObject.Instantiate(ItemPrefas);
                RectTransform _rtransform = _itemPrefas.GetComponent<RectTransform>();

                _itemPrefas.transform.SetParent(transform);
                _itemPrefas.transform.localPosition = Vector3.zero;
                _itemPrefas.transform.localScale = new Vector3(1, 1, 1);
                transform.GetChild(0).GetComponent<CSSIteamDrag>().SetItem(item);
                //_rtransform.offsetMax = new Vector2(-5, -5);
                //_rtransform.offsetMin = new Vector2(5, 5);
            }
            else
            {
                transform.GetChild(0).GetComponent<CSSIteamDrag>().AddAmount();
            }
        }
        public ItemType GetItemType()
        {
            return transform.GetChild(0).GetComponent<CSItemUI>().item.Type;
        }
        public string GetItemName()
        {
            return transform.GetChild(0).GetComponent<CSItemUI>().item.Name;
        }
        public int GetItemID()
        {
            return transform.GetChild(0).GetComponent<CSItemUI>().item.ID;
        }
        public bool IsFilled()
        {
            CSItemUI itemui = transform.GetChild(0).GetComponent<CSItemUI>();
            return itemui.Amount >= itemui.item.Capacity;
        }
    }
}
