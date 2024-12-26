using GlobalSpace;
using ObjectiveSpace;
using ObjectSpace;
using System.Collections;
using UnityEngine;

public class FenceGate : InteractableItem
{
    [SerializeField] private bool hasHammer;
    [SerializeField] private string findHammerText;
    [SerializeField] private string hammerObjective;
    private bool hasShownObjective;
    protected override void BeginInteraction()
    {
        if (hasHammer)
        {
            Debug.Log("Break fence");
            //New objective: find the sledgehammer;
            
        } else
        {
            PlayerThoughts.Instance.SetText(findHammerText);
            if (!hasShownObjective)
            {
                hasShownObjective = true;
                StartCoroutine(DelayForObjective());
            }
            canInteractWith = true;

        }
    }

    IEnumerator DelayForObjective()
    {
        yield return new WaitForSeconds(2f);
        ObjectiveManager.Instance.ShowObjective(hammerObjective);
    }
}
