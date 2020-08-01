using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSitemHuman : CSItemBase
{
    public int Other { get; set; }
  public CSitemHuman(int iD, string name, ItemQuality quality, ItemType type, string description, int capacity, int buyPrice, int sellprice, string sprite,int other):base(iD,name,quality,type,description,capacity,buyPrice,sellprice,sprite)
    {
        this.Other = other;
        
    }
}
