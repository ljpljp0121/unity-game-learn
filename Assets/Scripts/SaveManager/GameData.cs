using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int souls;
    public SerializableDic<string, bool> skillTree;
    public SerializableDic<string, int> inventory;
    public List<string> equipmentId;
    public Vector3 transform;

    public GameData()
    {
        this.souls = 0;
        transform = new Vector3(9.58f, 13.47f, 0);
        inventory = new SerializableDic<string, int>();
        skillTree = new SerializableDic<string, bool>();
        equipmentId = new List<string>();
    }
}
