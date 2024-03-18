using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;

    public UI_ItemTooltip itemTooltip;
    public UI_StatToolTip statToolTip;
    public UI_CraftWindow craftWindow;
    public UI_SkillToolTip skillTooltip;

    private void Start()
    {
        SwitchTo(null);
        itemTooltip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWithKeyTo(characterUI);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SwitchWithKeyTo(skillTreeUI);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchWithKeyTo(craftUI);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SwitchWithKeyTo(optionsUI);
        }
    } 
    public void SwitchTo(GameObject menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if (menu != null)
        {
            menu.SetActive(true);
        }
    }

    public void SwitchWithKeyTo(GameObject menu)
    {
        if (menu != null && menu.activeSelf)
        {
            menu.SetActive(false);
            return;
        }

        SwitchTo(menu);
    }
}
