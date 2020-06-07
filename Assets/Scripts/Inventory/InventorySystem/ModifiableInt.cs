﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModifiedEvent();
[System.Serializable]
public class ModifiableInt 
{
    [SerializeField]
    private int baseValue;
    public int BaseValue { get => baseValue; set { baseValue = value; UpdateModifiedValue(); } }

    [SerializeField]
    private int modifiedValue;
    public int ModifiedValue { get => modifiedValue; set => modifiedValue = value; }

    public List<IModifier> modifiers = new List<IModifier>();

    public ModifiedEvent ValueModified;
    public ModifiableInt(ModifiedEvent method = null)
    {
        modifiedValue = BaseValue;
        if (method != null)
        {
            ValueModified += method;
        }
    }
    public void RegisterModifiedEvent(ModifiedEvent method)
    {
        ValueModified += method;
    }
    public void UnregisterModifiedEvent(ModifiedEvent method)
    {
        ValueModified -= method;
    }
    public void UpdateModifiedValue()
    {
        var valueToAdd = 0;
        for (int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].AddValue(ref valueToAdd);
        }
        ModifiedValue = baseValue + valueToAdd;
        ValueModified?.Invoke();
    }
    public void AddModifier(IModifier _modifier)
    {
        modifiers.Add(_modifier);
        UpdateModifiedValue();
    }
    public void RemoveModifier(IModifier _modifier)
    {
        modifiers.Remove(_modifier);
        UpdateModifiedValue();
    }
}