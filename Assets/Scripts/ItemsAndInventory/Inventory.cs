using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventoryDic;

    [Header("Inventory UI")]

    [SerializeField] private Transform inventorySloParent;
    private UI_ItemSlot[] itemSlots;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDic = new Dictionary<ItemData, InventoryItem>();

        itemSlots = inventorySloParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    private void UpdateSlotUI()
    {
        for(int i = 0; i <inventoryItems.Count; i++)
        {
            itemSlots[i].UpdateSlot(inventoryItems[i]);
        }
    }
    public void AddItem(ItemData item)
    {
        if (inventoryDic.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            inventoryItems.Add(newItem);
            inventoryDic.Add(item, newItem);
        }
        UpdateSlotUI();
    }

    public void RemoveItem(ItemData item)
    {
        if (inventoryDic.TryGetValue(item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventoryDic.Remove(item);
            }
            else
            {
                value.RemoveStack();
            }
        }
        UpdateSlotUI();
    }
   
}
