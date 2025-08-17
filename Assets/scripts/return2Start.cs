using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class return2Holder : MonoBehaviour
{

    public Transform holderPosition;
    private XRGrabInteractable grabInteractable;

    private bool isGrabbed = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = holderPosition.position;
        transform.rotation = holderPosition.rotation;

        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(ReturnToStart);

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

    }

    // Update is called once per frame
    private void ReturnToStart(SelectExitEventArgs args)
    {
        goBack();
    }

    private void goBack()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;


        transform.position = holderPosition.position;
        transform.rotation = holderPosition.rotation;
    }

    private void Update()
    {
        if (transform.position != holderPosition.position && isGrabbed != true)
        {
            goBack();
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("simple");
        isGrabbed = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

}

