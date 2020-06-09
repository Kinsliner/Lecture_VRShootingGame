using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	//交由外部設定的參數
	/// <summary>
	/// 射擊槍口
	/// </summary>
	public Transform shootPoint;
	/// <summary>
	/// 射擊距離
	/// </summary>
	public float distance;
	/// <summary>
	/// 射擊間隔
	/// </summary>
	public float fireRate;
	/// <summary>
	/// 射擊傷害
	/// </summary>
	public int damage;
	/// <summary>
	/// 槍口開火特效
	/// </summary>
	public GameObject muzzleEffect;
	/// <summary>
	/// 場景擊中特效
	/// </summary>
	public GameObject impactWallEffect;
	/// <summary>
	/// 敵人擊中特效
	/// </summary>
	public GameObject impactEnemyEffect;
	/// <summary>
	/// 開火音效
	/// </summary>
	public AudioClip fireSound;

	//只在程式內部使用的參數
	/// <summary>
	/// 音效元件，初始化的時候由程式產生
	/// </summary>
	private AudioSource audioSource;
	/// <summary>
	/// 計算當前開火間隔的時間
	/// </summary>
	private float tickRate;
	/// <summary>
	/// 當前的開火狀態
	/// </summary>
	private bool state;

	private void Start()
	{
		//動態增加一個音效元件在這個物件上
		audioSource = gameObject.AddComponent<AudioSource>();
	}

	/// <summary>
	/// 外部設定當前的射擊狀態
	/// </summary>
	public void SetFireState(bool state)
	{
		this.state = state;
	}

	// Update is called once per frame
	void Update () {

		//當開火間隔小於零並且開火狀態為"是"的時候，執行開火的行為並重置開火間隔
		if (tickRate <= 0 && state == true)
		{
			tickRate = fireRate;
			Fire();
		}
		else
		{
			tickRate -= Time.deltaTime;
		}
	}

	/// <summary>
	/// 開火功能
	/// </summary>
	private void Fire()
	{
		//在槍口生成開火特效
		SpawnEffect(muzzleEffect, shootPoint.position, shootPoint.rotation, 0.1f);

		//從槍口打出一條虛擬射線
		RaycastHit hit;
		if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
		{
			Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
			if (enemy != null)
			{
				//如果射線擊中的是敵人，生成一個擊中敵人特效，並且對該敵人造成傷害
				SpawnEffect(impactEnemyEffect, hit.point, Quaternion.LookRotation(hit.normal), 1f);
				enemy.ApplyDamage(damage);
			}
			else
			{
				//如果射線擊中的不是敵人(擊中的是場景)，生成一個擊中場景特效
				SpawnEffect(impactWallEffect, hit.point, Quaternion.LookRotation(hit.normal), 1f);
			}

		}

		//播放開火音效
		audioSource.PlayOneShot(fireSound);
	}

	/// <summary>
	/// 生成特效功能
	/// </summary>
	private void SpawnEffect(GameObject prefab, Vector3 position, Quaternion rotation, float destroyTime)
	{
		//使用Instantiate生成一個物件
		GameObject effect = Instantiate(prefab, position, rotation);
		//在一定時間後刪除物件
		Destroy(effect, destroyTime);
	}
}
