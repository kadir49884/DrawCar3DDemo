using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawManager : MonoBehaviour
{
	[SerializeField] private bool canDraw;
	[SerializeField] private List<Vector2> points = new List<Vector2>();
	[SerializeField] private float sensitivity;
	[SerializeField] private LineRenderer line;
	[SerializeField] private LayerMask layerMask;

	private RaycastHit hit;
	private Ray ray;

	private bool drawStart = false;
	private int edgeIndex;

	private Vector3 currentPosition;
	private Vector3 startPosition;

	private Vector3 localHitPoint;

	private Camera mainCamera;

	public bool CanDraw { get => canDraw; set => canDraw = value; }
    public List<Vector2> Points { get => points; }

	private DrawMesh drawMesh;

    private void Awake()
	{
		mainCamera = Camera.main;
		InitDraw();
	}
	public void InitDraw()
	{
		CanDraw = true;
		edgeIndex = 0;
	}
	private void Start()
    {
		drawMesh = DrawMesh.Instance;
		Time.timeScale = 1f;
	}

	public void Update()
	{
		if (!CanDraw)
		return;

		if (Input.GetMouseButton(0))
		{
			ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 1000, layerMask))
			{
				localHitPoint = line.transform.InverseTransformPoint(hit.point);

				if (!drawStart)
				{
					ResetLine();
					startPosition = localHitPoint;

				}

				currentPosition = localHitPoint;

				if (Vector3.Distance(startPosition, currentPosition) >= sensitivity)
				{
					Points.Add(localHitPoint);

					line.positionCount = Points.Count;
					edgeIndex = Points.Count;

					line.SetPosition(edgeIndex - 1, localHitPoint);
					startPosition = currentPosition;
					Time.timeScale = 0.4f;
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			Time.timeScale = 1f;
			if(Points.Count > 3)
            {
				drawStart = false;
				drawMesh.UpdateDraw(points);
				ResetLine();
			}
		}
	
	}

	private void LateUpdate()
	{
		if (!CanDraw)
			return;

		line.material.SetTextureScale("_MainTex", new Vector2(GetLineLenght() / 5f, 0.1f));
	}

	private void ResetLine()
	{
		Points.Clear();
		line.positionCount = 0;
		edgeIndex = 0;
		drawStart = true;

	}

	public float GetLineLenght()
	{
		float distance = 0;
		for (int i = 0; i < line.positionCount - 1; i++)
		{
			distance += Vector3.Distance(line.GetPosition(i), line.GetPosition(i + 1));
		}
		return distance;
	}
	
}
