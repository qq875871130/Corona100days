using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VR_simpleGrab : MonoBehaviour
{
    private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    protected virtual void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
    }

    protected virtual void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabTypes = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        if (interactable.attachedToHand != null && grabTypes != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabTypes);
            hand.HoverLock(interactable);
        }
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }

    }


}
