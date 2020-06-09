using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardInput : MonoBehaviour {

	public float mouseSensitivity = 200;
	public float height = 1.7f;
	public float minLookAngle = -90;
	public float maxLookAngle = 90;

	private Gun gun;
	private Camera cam;
	private float xRot;
	private float yRot;

	// Use this for initialization
	void Start () {

		gun = FindObjectOfType<Gun>();
		cam = FindObjectOfType<Camera>();
		cam.transform.position = new Vector3(cam.transform.position.x, height, cam.transform.position.z);

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

		UpdateCameraLook();

		UpdateGunState();
	}

	private void UpdateCameraLook()
	{
		float x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		yRot += x;
		xRot -= y;

		xRot = Mathf.Clamp(xRot, minLookAngle, maxLookAngle);

		cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
	}

	private void UpdateGunState()
	{
		bool state = Input.GetMouseButton(0);
		gun.SetFireState(state);
	}
}
