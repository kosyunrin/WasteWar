using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SK
{
    public class CSOtherPartController : CSitemHuman
    {
        public CSOtherPartController(int iD, string name, ItemQuality quality, ItemType type, string description, int capacity, int buyPrice,
              int sellprice, string sprite, string renderMaterial, string showMonoPath, string moveMonoPath, BodyType btype, int other) : base(iD, name, quality, type, description, capacity, buyPrice, sellprice, sprite, renderMaterial, showMonoPath, moveMonoPath, btype,other)
        {

        }
    }
}
