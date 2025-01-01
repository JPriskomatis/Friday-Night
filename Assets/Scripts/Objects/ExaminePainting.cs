using GlobalSpace;
using ObjectSpace;
using System;
using UnityEngine;

public class ExaminePainting : InteractableItem
{
    public static event Action OnExamine;
    protected override void BeginInteraction()
    {
        PlayerThoughts.Instance.SetText("Seems like someone scratch the faces off of these paintings");
        OnExamine?.Invoke();
    }
}
