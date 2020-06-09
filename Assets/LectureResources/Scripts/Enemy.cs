using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public int hp = 100;
	public int attackPower = 10;
	public float attackRate = 1f;

	private Player player;
	private Animator animator;
	private NavMeshAgent agent;
	private AttackEventReciver attackEvent;
	private int currentHp;
	private float tickRate;

	// Use this for initialization
	void Start () {

		currentHp = hp;
		player = FindObjectOfType<Player>();
		animator = GetComponentInChildren<Animator>();
		agent = GetComponent<NavMeshAgent>();
		attackEvent = GetComponentInChildren<AttackEventReciver>();
	}
	
	// Update is called once per frame
	void Update () {

		if (currentHp > 0)
		{
			Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
			float distance = Vector3.Distance(transform.position, targetPosition);
			agent.SetDestination(player.transform.position);
			if (distance <= agent.stoppingDistance)
			{
				Stop();
				Attack();
			}
			else
			{
				Move();
			}
		}else
		{
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Disappear"))
			{
				agent.enabled = false;
				transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
				if(transform.position.y <= -1)
				{
					Destroy(gameObject);
				}
			}
		}
	}

	private void Move()
	{
		animator.SetBool("isStop", false);
		animator.CrossFade("Run", 0.25f);
		agent.radius = 0.5f;
	}

	private void Stop()
	{
		agent.isStopped = true;
		agent.velocity = Vector3.zero;
		animator.SetBool("isStop", true);
		agent.radius = 0;
	}

	private void Attack()
	{
		if (tickRate <= 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") )
		{
			int ran = Random.Range(0, 2);
			string attackAnimationName = ran == 0 ? "Attack1" : "Attack2";
			animator.CrossFade(attackAnimationName, 0.1f);

			tickRate = attackRate;
		}
		else if (tickRate > 0)
		{
			tickRate -= Time.deltaTime;
		}
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
		Stop();
		animator.CrossFade("Dead", 0.25f);
	}

	public void SendDamage()
	{
		player.ApplyDamage(attackPower);
	}
}
