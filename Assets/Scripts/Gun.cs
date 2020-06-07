using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Transform shootPoint;
	public float distance;
	public int damage;
	public GameObject muzzleEffect;
	public GameObject impactEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0))
		{
			Fire();
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
				enemy.ApplyDamage(damage);
			}
			else
			{
				SpawnEffect(impactEffect, hit.point, Quaternion.LookRotation(hit.normal), 1f);
			}

		}
	}

	private void SpawnEffect(GameObject prefab, Vector3 position, Quaternion rotation, float destroyTime)
	{
		GameObject effect = Instantiate(prefab, position, rotation);
		Destroy(effect, destroyTime);
	}
}
