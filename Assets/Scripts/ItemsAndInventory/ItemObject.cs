using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private ItemData ItemData;
    [SerializeField] private Rigidbody2D rb;

    private void SetupVisuals()
    {
        if (ItemData == null)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = ItemData.itemIcon;
        gameObject.name = "Item Object--" + ItemData.itemName;
    }

    public void SetupItem(ItemData itemData,Vector2 velocity)
    {
        this.ItemData = itemData;

        rb.velocity = velocity;
        SetupVisuals();
    }
    

    public void PickupItem()
    {
        if (!Inventory.Instance.CanAddItem() && ItemData.itemType == ItemType.Equipment)
        {
            rb.velocity = new Vector2(0,7);
            return;
        }

        Inventory.Instance.AddItem(ItemData);
        Destroy(gameObject);
    }
}
