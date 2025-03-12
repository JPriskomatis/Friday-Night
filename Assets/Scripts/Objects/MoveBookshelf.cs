using System.Collections;
using UnityEngine;

public class MoveBookshelf : MonoBehaviour
{
    
    public float rotationAmount = 100f;
    // Duration of the rotation
    public float duration = 5f;

    public void RotateSelf()
    {
        // Start the coroutine to rotate the object
        StartCoroutine(RotateOverTime());
    }

    IEnumerator RotateOverTime()
    {
        // Record the starting rotation
        Quaternion startRotation = transform.rotation;
        // Calculate the target rotation
        Quaternion endRotation = startRotation * Quaternion.Euler(0, -rotationAmount, 0);

        // Track the time elapsed
        float timeElapsed = 0;

        // Rotate the object over the specified duration
        while (timeElapsed < duration)
        {
            // Calculate how far to rotate based on the time passed
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the object ends exactly at the target rotation
        transform.rotation = endRotation;
    }
}
