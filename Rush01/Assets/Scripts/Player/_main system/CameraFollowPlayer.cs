using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
	private float	currentZoom = 8f;
	private float 	cameraYaw;

	private Transform	target;
	public Vector3		offset;
	public float		maxZoom = 15f;
	public float		minZoom = 5f;
	public float		cameraYPosition = 2f;
	public float		zoomSpeed = 2f;
	public float 		cameraYawSpeed = 50f;

	void Start()
	{
		target = GameObject.FindWithTag("Player").transform;
	}

	void Update()
	{
		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

		cameraYaw -= Input.GetAxis("Horizontal") * cameraYawSpeed * Time.deltaTime;
	}

	void LateUpdate()
	{
		transform.position = target.position - offset * currentZoom;
		transform.LookAt(target.position + Vector3.up * cameraYPosition);

		transform.RotateAround(target.position, Vector3.up, cameraYaw);
	}
}
