using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour {

	private Gun gun;

	// Use this for initialization
	void Start () {

		gun = FindObjectOfType<Gun>();
	}

	// Update is called once per frame
	void Update()
	{
		bool state = SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any);
		gun.SetFireState(state);
	}
}
