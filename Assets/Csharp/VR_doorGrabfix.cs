using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// 门把手速度修正
/// </summary>
public class VR_doorGrabfix : MonoBehaviour
{
    public Transform handler;
    Rigidbody rigidbody;
    Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        rigidbody = handler.GetComponent<Rigidbody>();
    }

    protected virtual void OnDetachedFromHand(Hand hand)
    {
            transform.position = handler.transform.position;
            transform.rotation = handler.transform.rotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

    }

    protected virtual void OnAttachedToHand(Hand hand)
    {
        Debug.Log(Vector3.Distance(handler.transform.position, transform.position).ToString());

        if (Vector3.Distance(handler.transform.position, transform.position) > 0.4f)
        {
            hand.DetachObject(gameObject);
        }
    }


}
