using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{

    protected override void Start()
    {
        base.Start();
    }
    
    public void SetUpCraftSlot(ItemData_Equipment data)
    {
        if(data == null)
        {
            return;
        }
        item.data = data;

        itemImage.sprite = data.itemIcon;
        itemText.text = data.itemName;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
    }
}
