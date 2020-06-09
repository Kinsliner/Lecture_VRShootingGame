using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int hp = 300;

	private int currentHp;

	private void Start()
	{
		currentHp = hp;
	}

	public void ApplyDamage(int damage)
	{
		if(currentHp > 0)
		{
			currentHp -= damage;
			if(currentHp <= 0)
			{
				currentHp = 0;
				Dead();
			}
		}
	}

	private void Dead()
	{
		Debug.Log("Game Over");
	}
}
