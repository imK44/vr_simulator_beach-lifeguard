using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LifeBuoyManager : MonoBehaviour
{
    public GameObject lifeBuoyPrefab; // Assign the Life Buoy prefab
    public Transform spawnPoint; // Assign the spawn location

    private void Start()
    {
        // Ensure the first buoy is available at the start
        SpawnNewBuoy();
    }

    public void SpawnNewBuoy()
    {
        Debug.Log("Spawning a new Life Buoy...");

        // Spawn a new buoy and assign the manager
        GameObject newBuoy = Instantiate(lifeBuoyPrefab, spawnPoint.position, spawnPoint.rotation);
        newBuoy.GetComponent<LifeBuoyThrow>().SetManager(this);

        // Listen for when it gets picked up
        XRGrabInteractable grabInteractable = newBuoy.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnBuoyPickedUp);
        }
    }

    private void OnBuoyPickedUp(SelectEnterEventArgs args)
    {
        Debug.Log("Life Buoy picked up! Spawning another...");
        SpawnNewBuoy(); // Immediately spawn another buoy when picked up
    }
}
