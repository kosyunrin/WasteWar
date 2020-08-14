using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SK
{
    public interface ItemUI
    {
        void CreateShowMono();
        void CreateMoveMono();
        void DeleteShowMono();
        void DeleteMoveMono();
        void ChangeShowMono();

    }
    public class CSItemUI : MonoBehaviour
    {

        public int Amount { get; set; }
        public CSItemBase item { get; set; }
        public Text ItemText;
        protected Image ItemIcon;
        //protected GameObject ShowRenderCamera = null;
        protected GameObject ShowMono = null;
        protected GameObject MoveMono = null;
       // protected Material ShowMaterial = null;
    }
}
