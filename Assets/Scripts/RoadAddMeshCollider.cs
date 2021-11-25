using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadAddMeshCollider : MonoBehaviour
{

    private void Start()
    {
        transform.gameObject.AddComponent<MeshCollider>();
    }
}
