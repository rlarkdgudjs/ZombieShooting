using NUnit.Framework.Interfaces;
using UnityEngine;

public class SlotData 
{
    public SO_ItemData item = null;
    public int quantity = 1;

    public bool IsEmpty => item == null;
}
