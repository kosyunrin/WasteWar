using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK.KNAPSACK
{

    public class CSitemAnimals : CSItemBase
    {
        public int Other { get; set; }
        public CSitemAnimals(int iD, string name, ItemQuality quality, ItemType type, string description, 
            int capacity, int buyPrice, int sellprice, string sprite, string renderMaterial, string showMonoPath, string moveMonoPath, BodyType btype, int other) : base(iD, name, quality, type, description, capacity, buyPrice, sellprice, sprite , renderMaterial,  showMonoPath,  moveMonoPath, btype)
        {
            this.Other = other;

        }
    }
}
