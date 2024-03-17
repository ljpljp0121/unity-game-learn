using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_ItemSlot : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemText;

    protected UI ui;

    public InventoryItem item;

    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();
    }
    public void UpdateSlot(InventoryItem item)
    {
        this.item = item;

        itemImage.color = Color.white;
        if (item != null)
        {
            itemImage.sprite = item.data.itemIcon;
            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;

        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.Instance.RemoveItem(item.data);
            return;
        }

        if(item.data.itemType == ItemType.Equipment)
        {
            Inventory.Instance.EquipItem(item.data);
        }

        ui.itemTooltip.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;
        ui.itemTooltip.ShowToolTip(item.data as ItemData_Equipment);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null)
            return;
        ui.itemTooltip.HideToolTip();
    }
}
