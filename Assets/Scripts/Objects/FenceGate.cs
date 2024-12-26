using GlobalSpace;
using ObjectiveSpace;
using ObjectSpace;
using System.Collections;
using UnityEngine;

public class FenceGate : InteractableItem
{
    [Header("Hammer Settings")]
    [SerializeField] private bool hasHammer;
    [SerializeField] private string findHammerText;
    [SerializeField] private string hammerObjective;
    [SerializeField] private AudioClip hammerSound;
    private bool hasShownObjective;

    

    private void OnEnable()
    {
        Sledgehammer.OnTakeHammer += HasHammer;
    }

    private void OnDisable()
    {
        Sledgehammer.OnTakeHammer -= HasHammer;
    }

    private void HasHammer()
    {
        hasHammer = true;
    }
    protected override void BeginInteraction()
    {
        if (hasHammer)
        {
            Debug.Log("Break fence");
            //New objective: find the sledgehammer;
            EnableBlackScreen.Instance.StartBlackScreen(hammerSound);

            
            Destroy(gameObject,2f);
            
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
