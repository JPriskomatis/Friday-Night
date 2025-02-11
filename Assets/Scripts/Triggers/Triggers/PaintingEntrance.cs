using ObjectSpace;
using TriggerSpace;
using UnityEngine;

public class PaintingTrigger : FloorTrigger
{
    [SerializeField] private Door door;
    protected override void InitiateAction()
    {
        interactAgain = false;
        door.PublicCloseDoor();
        door.canOpen = false;
    }
}
