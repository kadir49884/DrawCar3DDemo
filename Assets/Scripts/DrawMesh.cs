using ProBuilder2.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DrawMesh : MonoBehaviour
{

    [SerializeField] private Material material;
    [SerializeField] private GameObject frontWheels;
    [SerializeField] private GameObject backWheels;

    private static DrawMesh instance;
    private Quaternion defaultRot;

    private Rigidbody rb;

    public static DrawMesh Instance { get => instance; set => instance = value; }

    private GameObject createCar;
    private Mesh mesh;
    private MeshCollider meshCollider;

    private Vector3 frontPos;
    private Vector3 backPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        defaultRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }


    public void UpdateDraw(List<Vector2> points)
    {

        for (int i = 0; i < points.Count; i++)
        {
            points[i] *= 1000;
        }
        if(transform.childCount > 1)
        {
            Destroy(transform.GetChild(1).gameObject);
        }

        createCar = new GameObject();
        createCar.transform.parent = transform;
        createCar.transform.position = transform.position;
        transform.position = transform.position + Vector3.up * 4;
        createCar.transform.DOLocalRotate(new Vector3(0, -90, 0), 0.01f).SetUpdate(UpdateType.Fixed);

        if (transform.eulerAngles.x < -7 || transform.eulerAngles.x > 7)
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.01f).SetUpdate(UpdateType.Fixed);
        }


        var carObject = createCar.AddComponent<pb_Object>();
        pb_BezierShape pathObject = createCar.AddComponent<pb_BezierShape>();
        List<pb_BezierPoint> beizers = BeizerListCreate(points);
        pathObject.m_Points = beizers;
        pathObject.m_Radius = 25;
        pathObject.Refresh();
        createCar.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        carObject.ToMesh();
        mesh = createCar.GetComponent<MeshFilter>().sharedMesh;
        meshCollider = createCar.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshCollider.convex = true;
        createCar.GetComponent<MeshRenderer>().material = material;
        WheelPosition(createCar.transform, pathObject);

        rb.angularVelocity = new Vector3(0f, 0f, 0f);
        transform.rotation = defaultRot;

    }

 

    private List<pb_BezierPoint> BeizerListCreate(List<Vector2> points)
    {
        List<pb_BezierPoint> beizerPoints = new List<pb_BezierPoint>();
        for (int i = 0; i < points.Count; i++)
        {
            beizerPoints.Add(new pb_BezierPoint(points[i], points[i], points[i], Quaternion.identity));
        }
        return beizerPoints;
    }

    public void WheelPosition(Transform parent, pb_BezierShape pathObject)
    {
        frontPos = pathObject.m_Points[pathObject.m_Points.Count - 1].position * parent.localScale.x;
        frontPos.z = frontPos.x;
        frontPos.x = 0;
        backPos = pathObject.m_Points[0].position * 0.01f;
        backPos.z = backPos.x;
        backPos.x = 0;
        frontWheels.transform.localPosition = frontPos;
        backWheels.transform.localPosition = backPos;
    }
}
