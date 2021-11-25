using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;
	[SerializeField] private float lerpTime = .1f;


	void FixedUpdate()
	{
		if (target)
			transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpTime);
	}

}
