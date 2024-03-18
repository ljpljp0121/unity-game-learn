using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private UI ui;
    [SerializeField] private string skillName;
    [SerializeField] private string skillDescription;
    [SerializeField] private Color lockedSkillColor;


    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;

    [SerializeField] private Image skillImage;

    private void OnValidate()
    {
        gameObject.name = "技能" + skillName;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
    }
    private void Start()
    {
        skillImage = GetComponent<Image>();
        ui = GetComponentInParent<UI>();

        skillImage.color = lockedSkillColor;
    }

    public void UnlockSkillSlot()
    {
        //需要解锁前置技能
        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (shouldBeUnlocked[i].unlocked == false)
            {
                return;
            }
        }
        //需要遗忘特定技能
        for (int i = 0; i < shouldBeLocked.Length; i++)
        {
            if (shouldBeLocked[i].unlocked == true)
            {
                return;
            }
        }

        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillTooltip.ShowToolTip(skillDescription, skillName);
        Vector2 mousePosition = Input.mousePosition;

        float xOffset = 0;
        float yOffset = 0;
        if (mousePosition.x > 600)
        {
            xOffset = -150;
        }
        else
        {
            xOffset = 150;
        }

        if (mousePosition.y > 600)
        {
            yOffset = -150;
        }
        else
        {
            yOffset = 150;
        }

        ui.skillTooltip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillTooltip.HideToolTip();
    }
}
