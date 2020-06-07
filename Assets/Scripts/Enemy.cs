using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public int hp = 100;
	public int attackPower;

	private Animator animator;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ApplyDamage(int damage)
	{

	}
}
