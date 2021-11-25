using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{

    [SerializeField]
    private Transform chechPoint;

    private Vector3 chechPos;
    private GameObject otherObject;

    private void Start()
    {
        chechPos = chechPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        otherObject = other.gameObject.transform.parent.parent.gameObject;
        otherObject.SetActive(false);
        otherObject.transform.DORotate(new Vector3(0, 0, 0), 0.1f);
        otherObject.transform.DOMove(new Vector3(otherObject.transform.position.x, chechPos.y + 5, chechPos.z - 30), 1f)
            .SetUpdate(UpdateType.Fixed).OnComplete(() =>
            {
                otherObject.SetActive(true);
            });
    }
}
