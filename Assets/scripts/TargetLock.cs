using UnityEngine;

public class TargetLock : MonoBehaviour
{
    public Transform vrCamera; // Assign the VR Camera (Main Camera in XR Origin)
    public float maxLockDistance = 30f;
    public LayerMask targetLayer; // Assign the "DrowningPerson" layer
    private Transform lockedTarget;

    void Update()
    {
        RaycastHit hit;
        Vector3 rayOrigin = vrCamera.position;
        Vector3 rayDirection = vrCamera.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxLockDistance, targetLayer))
        {
            lockedTarget = hit.transform;
            Debug.Log("Locked onto: " + lockedTarget.name);
        }
        else
        {
            lockedTarget = null;
        }
    }

    public Transform GetLockedTarget()
    {
        return lockedTarget;
    }
}
