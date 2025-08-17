using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WhistleBlow : MonoBehaviour
{
    public Transform vrCamera; // Assign Main Camera (VR Player Head)
    public float blowDistance = 0.2f; // How close to the mouth it needs to be
    public AudioSource audioSource; // Assign whistle sound effect
    private XRGrabInteractable grabInteractable;
    private bool isBlowing = false;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();


        audioSource = GetComponent<AudioSource>();

        //// Detect when the player grabs/releases the whistle
        //grabInteractable.selectEntered.AddListener(OnGrab);
        //grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void Update()
    {

        bool isNearMouth = Vector3.Distance(transform.position, vrCamera.position) < blowDistance;
        bool isGrabbed = grabInteractable.isSelected; // Check if the player is holding the whistle

        if (isGrabbed && isNearMouth)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioSource.clip);  // Play whistle sound
            }
        }
        else
        {
            audioSource.Stop(); // Stop sound when moved away or released
        }
    }
}
//    private void OnGrab(SelectEnterEventArgs args)
//    {
//        isBlowing = true; // Whistle is in use
//    }

//    private void OnRelease(SelectExitEventArgs args)
//    {
//        isBlowing = false; // Whistle is no longer in use
//        audioSource.Stop(); // Ensure the sound stops
//    }
//}
