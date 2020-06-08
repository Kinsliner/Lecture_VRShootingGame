using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Transform shootPoint;
	public float distance;
	public float fireRate;
	public int damage;
	public GameObject muzzleEffect;
	public GameObject impactWallEffect;
	public GameObject impactEnemyEffect;
	public AudioClip fireSound;

	private AudioSource audioSource;
	private float tickRate;
	private bool state;

	private void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = fireSound;
	}

	public void SetFireState(bool state)
	{
		this.state = state;
	}

	// Update is called once per frame
	void Update () {

		if (tickRate <= 0 && state)
		{
			tickRate = fireRate;
			Fire();
		}
		else
		{
			tickRate -= Time.deltaTime;
		}
	}

	private void Fire()
	{
		SpawnEffect(muzzleEffect, shootPoint.position, shootPoint.rotation, 0.1f);

		RaycastHit hit;
		if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
		{
			Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
			if (enemy != null)
			{
				SpawnEffect(impactEnemyEffect, hit.point, Quaternion.LookRotation(hit.normal), 1f);
				enemy.ApplyDamage(damage);
			}
			else
			{
				SpawnEffect(impactWallEffect, hit.point, Quaternion.LookRotation(hit.normal), 1f);
			}

		}

		audioSource.PlayOneShot(fireSound);
	}

	private void SpawnEffect(GameObject prefab, Vector3 position, Quaternion rotation, float destroyTime)
	{
		GameObject effect = Instantiate(prefab, position, rotation);
		Destroy(effect, destroyTime);
	}
}
