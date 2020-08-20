using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK
{
    public enum ItemType
    {
        Human,
        animals,
        robot,
        Chimera
    }
    public enum ItemQuality
    {
        N,
        R,
        SR,
        SSR,
        UR
    }
    public enum BodyType
    {
        None,
        MainPart,
        OtherPart,
    }
    public interface IteamInterface
    {
        
    }
    [SerializeField]
    public class CSItemBase 
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual ItemQuality Quality { get; set; }
        public virtual ItemType Type { get; set; }
        public virtual string Description { get; set; }//説明
        public virtual int Capacity { get; set; }//容量
        public virtual int BuyPrice { get; set; }
        public virtual int Sellprice { get; set; }
        public virtual string Sprite { get; set; }
        public virtual string RenderMaterial { get; set; }
        public virtual string ShowMonoPath { get; set; }
        public virtual string MoveMonoPath { get; set; }
        public virtual BodyType BType { get; set; }
        // Start is called before the first frame update
        public CSItemBase(int iD, string name, ItemQuality quality, ItemType type, string description, int capacity, int buyPrice, 
            int sellprice, string sprite,string renderMaterial, string showMonoPath, string moveMonoPath,BodyType btype)
        {
            this.ID = iD;
            this.Name = name;
            this.Quality = quality;
            this.Type = type;
            this.Description = description;
            this.Capacity = capacity;
            this.BuyPrice = buyPrice;
            this.Sellprice = sellprice;
            this.Sprite = sprite;
            this.RenderMaterial = renderMaterial;
            this.ShowMonoPath = showMonoPath;
            this.MoveMonoPath = moveMonoPath;
            this.BType = btype;
        }

        // Update is called once per frame

    }
}

