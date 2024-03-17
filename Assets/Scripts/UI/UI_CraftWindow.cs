using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button craftButton;

    [SerializeField] private Image[] materialImage;

    public void SetupCraftWindow(ItemData_Equipment data)
    {
        craftButton.onClick.RemoveAllListeners();

        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }

        for (int i = 0; i < data.craftingMaterials.Count; i++)
        {
            materialImage[i].sprite = data.craftingMaterials[i].data.itemIcon;
            materialImage[i].color = Color.white;

            TextMeshProUGUI materialSlotText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();

            materialSlotText.text = data.craftingMaterials[i].stackSize.ToString();
            materialSlotText.color = Color.white;
        }

        itemIcon.sprite = data.itemIcon;
        itemName.text = data.itemName;
        itemDescription.text = data.GetDescription();

        craftButton.onClick.AddListener(() => Inventory.Instance.CanCraft(data, data.craftingMaterials));
    }
}
