using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class VR_playerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 TouchInput;
    public float Move_Speed = 1.0f;
    private CharacterController character;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        character = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchInput.axis.magnitude > 0.1f)
        {
            direction = Player.instance.hmdTransform.TransformDirection(new Vector3(TouchInput.axis.x, 0, TouchInput.axis.y));
            character.Move(Move_Speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));

        }
    }
}
