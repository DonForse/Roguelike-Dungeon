using UnityEngine;
using System.Collections;

public class ItemPotion : IItem
{
    public override void Use()
    {
        Debug.Log("Uso una pocion");
    }

    public override IItem CreateItem()
    {
        return new ItemPotion();
    }
}
