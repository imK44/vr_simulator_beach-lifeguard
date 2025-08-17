using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LifeBuoyThrow : MonoBehaviour
{
    public float throwSpeed = 20f;
    private Rigidbody rb;
    private TargetLock targetLock;
    private LifeBuoyManager manager; // Reference to the manager
    private XRGrabInteractable grabInteractable; // Detects when the buoy is released

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetLock = FindFirstObjectByType<TargetLock>(); // Get the targeting system
        grabInteractable = GetComponent<XRGrabInteractable>(); // Get grab system

        // Listen for when the buoy is released
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Buoy released!");
        ThrowBuoy(); // Throw when released
    }

    public void ThrowBuoy()
    {
        Transform target = targetLock.GetLockedTarget();
        if (target != null)
        {
            // Move the buoy toward the target
            rb.AddForce((target.position - transform.position).normalized * throwSpeed, ForceMode.VelocityChange);
        }
        else
        {
            // Normal physics-based throw
            rb.AddForce(Camera.main.transform.forward * throwSpeed, ForceMode.VelocityChange);
        }

        // Tell the manager to spawn a new buoy
        if (manager != null)
        {
            Debug.Log("Buoy thrown, requesting a new buoy...");
            manager.SpawnNewBuoy();
        }
        else
        {
            Debug.LogError("LifeBuoyManager is NULL! Make sure it is assigned correctly.");
        }

        // Destroy this buoy after throwing
        Destroy(gameObject, 3f);
    }

    public void SetManager(LifeBuoyManager buoyManager)
    {
        manager = buoyManager;
        Debug.Log("LifeBuoyManager assigned to buoy.");
    }
}
