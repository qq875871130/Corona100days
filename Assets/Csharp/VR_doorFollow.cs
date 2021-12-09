using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_doorFollow : MonoBehaviour
{
    public Transform target;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.MovePosition(target.transform.position);
    }
}
