using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventReciver : MonoBehaviour {

	//接受從動畫傳出的事件
	public void AttackEvent()
	{
		//執行在父物件上的程式中名為"SendDamage"的功能
		SendMessageUpwards("SendDamage");
	}
}
