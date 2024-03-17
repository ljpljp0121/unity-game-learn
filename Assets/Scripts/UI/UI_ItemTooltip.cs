using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;
    

    public void ShowToolTip(ItemData_Equipment item)
    {
        if(item == null)
            return;
        itemNameText.text = item.itemName;
        itemTypeText.text = item.equipmentType.ToString();
        itemDescription.text = item.GetDescription();

        

        gameObject.SetActive(true);
    }

    public void HideToolTip() => gameObject.SetActive(false);
}
