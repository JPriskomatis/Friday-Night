using UnityEngine;

[CreateAssetMenu(fileName = "TargetToFollow", menuName = "Scriptable Objects/TargetToFollow")]
public class TargetToFollow : ScriptableObject
{
    public GameObject target;

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
