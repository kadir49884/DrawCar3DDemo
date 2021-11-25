using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{


    [SerializeField]
    private WheelCollider[] wheelColliders;

    [SerializeField]
    private float torquePower;
    [SerializeField]
    private float power;
    [SerializeField]
    private float carMaxSpeed = 20;

    private Rigidbody rb;
    private GameManager gameManager;


    [SerializeField]
    private Transform lineTransform;

    [SerializeField]
    private Transform playerPos;

    private float distance;

    [SerializeField]
    private bool isPlayer;

    private Vector3 tempPos;

    private GameObject finishObject;

    private float finishPoint;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
        gameManager.GameStart += GameStart;

    }


    private void GameStart()
    {
        transform.GetComponent<Rigidbody>().useGravity = true;
        finishObject = finishObject = GameObject.FindGameObjectWithTag("FinishObject");
        finishPoint = finishObject.transform.position.z/10;
        tempPos = lineTransform.localPosition;
    }

    public void FixedUpdate()
    {

        if (!gameManager.ExecuteGame)
        {
            return;
        }
        
        tempPos.x = transform.position.z / finishPoint;
        lineTransform.localPosition = Vector3.Lerp(lineTransform.localPosition, tempPos, 0.1f);

        distance = Vector3.Distance(transform.position, playerPos.position);
        if (distance > 50 && transform.position.z > playerPos.position.z)
        {
            carMaxSpeed = 5;
        }
        else if(!isPlayer)
        {
            carMaxSpeed = 20;
        }

        if (rb.velocity.magnitude < carMaxSpeed)
        {
            foreach (var item in wheelColliders)
            {
                item.motorTorque = Time.deltaTime * torquePower;
            }
            if (IsGround())
            {
                rb.AddForce(Vector3.forward * Time.deltaTime * power, ForceMode.Acceleration);
            }
        }
        if (rb.angularVelocity.magnitude > 5)
        {
            rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.deltaTime * 10);
        }
    }

    private bool IsGround()
    {
        foreach (var item in wheelColliders)
        {
            if (!item.isGrounded)
            {
                return false;
            }
        }
        return true;
    }

}
