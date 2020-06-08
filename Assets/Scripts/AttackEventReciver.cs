using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventReciver : MonoBehaviour {

	public void AttackEvent()
	{
		SendMessageUpwards("SendDamage");
	}
}
