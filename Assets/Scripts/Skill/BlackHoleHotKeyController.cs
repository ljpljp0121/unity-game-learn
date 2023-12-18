using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackHoleHotKeyController : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;
    private Transform enemiesTransform;
    private BlackHoleSkillController blackHole;
    public void SetupHotKey(KeyCode hotKey,Transform myEnemy,BlackHoleSkillController blackHole)
    {
        sr = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        enemiesTransform = myEnemy;
        this.blackHole = blackHole;

        myHotKey = hotKey;
        myText.text = myHotKey.ToString();
    }
    private void Update()
    {
        if(Input.GetKeyDown(myHotKey))
        {
            blackHole.AddEnemyToList(enemiesTransform);

            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}  
