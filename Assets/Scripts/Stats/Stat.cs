using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;

    public List<int> modifiers;
    public int GetValue()
    {
        int finalValue = baseValue;
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        this.modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        this.modifiers.RemoveAt(modifier);
    }

    public void SetDefaultValue(int value)
    {
        baseValue= value;
    }
}
