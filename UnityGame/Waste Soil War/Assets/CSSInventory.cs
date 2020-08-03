using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK.KNAPSACK
{
    public class CSSInventory : MonoBehaviour
    {
        public static CSSInventory SharedInstance = null;
        public bool CanDrag = true;
        public GameObject LastShowmessageBox = null;
        public List<CSItemBase> Items = null;
        private CSSSlot[] Slots = null;

        private void Awake()
        {
            SharedInstance = this;
            CanDrag = true;
        }
        // Start is called before the first frame update
        void Start()
        {
            Slots = GetComponentsInChildren<CSSSlot>();
            Items = new List<CSItemBase>();
            Items.Add(new CSitemHuman(50, "Pj", ItemQuality.SR, ItemType.Human, "shuoming", 20, 50, 25, "Textures/Texture@UI/50",
                "Materials/PJ@Show@Material", "Prefabs/OperatingPart/OtherPart/Show/PjanicShow", "Prefabs/OperatingPart/OtherPart/Move/PjanicMove", BodyType.OtherPart, 100));
            Items.Add(new CSitemAnimals(40, "GuaWa", ItemQuality.SR, ItemType.animals, "shuoming", 20, 50, 25, "Textures/Texture@UI/40",
                "Materials/Material@Guaiwa@show", "Prefabs/OperatingPart/MainPart/Show/GuaWaShow", "Prefabs/OperatingPart/MainPart/Move/GuaWaMove", BodyType.MainPart, 100));
            Items.Add(new CSitemAnimals(30, "Zhizhu", ItemQuality.SR, ItemType.animals, "shuoming", 20, 50, 25, "Textures/Texture@UI/30",
                "Materials/Material@Spide@Show", "Prefabs/OperatingPart/MainPart/Show/SpiderShow", "Prefabs/OperatingPart/MainPart/Move/SpiderMove", BodyType.MainPart, 100));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                isPutInsideSlots(40);
                isPutInsideSlots(50);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                isPutInsideSlots(30);
            }
        }
        public CSItemBase GetItem(int iD)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                CSItemBase _item = Items[i];
                if (_item.ID == iD) return _item;
            }
            return null;
        }
        public bool isPutInsideSlots(int id)
        {
            CSItemBase item = CSSInventory.SharedInstance.GetItem(id);
            return isPutInsideSlots(item);
        }
        public bool isPutInsideSlots(CSItemBase item)
        {
            if (item == null) return false;
            if (item.Capacity == 1)
            {
                CSSSlot slot = FindEmptySlot();
                if (slot == null)
                {
                    Debug.LogWarning("mei kong de slot");
                    return false;
                }
                else
                {
                    slot.PutInside(item);
                }
            }
            else
            {
                CSSSlot _slot = FindSameIDSlot(item);
                if (_slot != null)
                {
                    _slot.PutInside(item);
                }
                else
                {
                    CSSSlot slot2 = FindEmptySlot();
                    if (slot2 != null) slot2.PutInside(item);
                    else
                    {
                        Debug.LogWarning("mei kongde ");
                        return false;
                    }
                }
            }
            return true;
        }
        private CSSSlot FindEmptySlot()
        {
            foreach (CSSSlot slot in Slots)
            {
                if (slot.transform.childCount == 0)
                    return slot;
            }
            return null;
        }
        private CSSSlot FindSameTypeSlot(CSItemBase Item)
        {
            foreach (CSSSlot slot in Slots)
            {
                if (slot.transform.childCount >= 1 && slot.GetItemType() == Item.Type && slot.IsFilled() == false)
                    return slot;
            }
            return null;
        }
        private CSSSlot FindSameNameSlot(CSItemBase Item)
        {
            foreach (CSSSlot slot in Slots)
            {
                if (slot.transform.childCount >= 1 && slot.GetItemName() == Item.Name && slot.IsFilled() == false)
                    return slot;
            }
            return null;
        }

        private CSSSlot FindSameIDSlot(CSItemBase Item)
        {
            foreach (CSSSlot slot in Slots)
            {
                if (slot.transform.childCount >= 1 && slot.GetItemID() == Item.ID && slot.IsFilled() == false)
                    return slot;
            }
            return null;
        }
    }
}
