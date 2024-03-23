using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int souls;
    public void LoadData(GameData data)
    {
        this.souls = data.souls;
    }

    public void SaveData(ref GameData data)
    {
        data.souls =this.souls;
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
        instance = this;
        }
    }
}
