using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CrosshairController : MonoBehaviour
{
    public GameObject crosshair;
    public XRGrabInteractable currentBuoy;

    private void Start()
    {
        crosshair.SetActive(false); // Hide at start

    }

    private void Update()
    {
        XRGrabInteractable[] allInteractables = FindObjectsByType<XRGrabInteractable>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        foreach (var interactable in allInteractables)
        {
            // Check if the object has the "LifeBuoy" tag
            if (interactable.gameObject.CompareTag("LifeBuoy"))
            {
                if (interactable.isSelected) // If the life buoy is currently being held
                {
                    if (currentBuoy != interactable)
                    {
                        AttachToBuoy(interactable);
                    }
                }
            }
        }
    }

    private void AttachToBuoy(XRGrabInteractable buoy)
    {
        if (currentBuoy != null)
        {
            currentBuoy.selectExited.RemoveListener(HideCrosshair);
        }

        currentBuoy = buoy;
        currentBuoy.selectExited.AddListener(HideCrosshair);
        ShowCrosshair();
    }
    private void ShowCrosshair()
    {
        crosshair.SetActive(true);
    }

    private void HideCrosshair(SelectExitEventArgs args)
    {
        crosshair.SetActive(false);
    }
}
