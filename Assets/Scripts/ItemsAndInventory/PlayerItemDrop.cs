using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player drop")]
    [SerializeField] private float chanceToLooseItems;
    [SerializeField] private float chanceToLooseMaterials;
    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.Instance;


        List<InventoryItem> currentStash = Inventory.Instance.GetStashList();
        List<InventoryItem> currentEquipment = Inventory.Instance.GetEquipmentList();
        List<InventoryItem> itemsToUnequip = new List<InventoryItem>();
        List<InventoryItem> materialsToLoose = new List<InventoryItem>();

        for (int i = 0; i < currentEquipment.Count; i++)
        {
            if (Random.Range(0, 100) <= chanceToLooseItems)
            {
                DropItem(currentEquipment[i].data);
                itemsToUnequip.Add(currentEquipment[i]);
            }
        }

        for (int i = 0; i < currentStash.Count; i++)
        {
            if (Random.Range(0, 100) <= chanceToLooseItems)
            {
                DropItem(currentStash[i].data);
                materialsToLoose.Add(currentStash[i]);
            }
        }

        for (int i = 0; i < itemsToUnequip.Count; i++)
        {
            inventory.UnequipItem(itemsToUnequip[i].data as ItemData_Equipment);
        }

        for (int i = 0; i < materialsToLoose.Count; i++)
        {
            inventory.UnequipItem(materialsToLoose[i].data as ItemData_Equipment);
        }

    }
}
