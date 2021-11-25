using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    private GameObject wheelParent;
    private WheelCollider wheelCollider;

    private Quaternion rot;
    private Vector3 pos;

    private void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
        wheelParent = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        WheelRotate();
    }
    private void WheelRotate()
    {
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelParent.transform.position = pos;
        wheelParent.transform.rotation = rot;
    }
}
