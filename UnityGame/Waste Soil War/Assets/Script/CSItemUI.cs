using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSItemUI : MonoBehaviour
{
    public int Amount { get; set; }
    public CSItemBase item { get; set; }
    private Text ItemText;
    private Image ItemImage;
    private void Awake()
    {
        ItemText = GetComponentInChildren<Text>();
        ItemImage = GetComponent<Image>();
    }
   public void SetItem(CSItemBase item,int amount=1)
    {
        this.item = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);

    }
    public void AddAmount(int amount=1)
    {
        Amount += amount;
        ItemText.text = Amount.ToString();
    }
}
