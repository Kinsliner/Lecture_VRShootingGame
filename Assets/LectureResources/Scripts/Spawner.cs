using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	//交由外部設定的參數
	/// <summary>
	/// 生成點的集合
	/// </summary>
	public Transform[] spawnPoints;
	/// <summary>
	/// 要生成的Prefab
	/// </summary>
	public GameObject spawnPrefab;
	/// <summary>
	/// 生成的間隔時間
	/// </summary>
	public float spawnInterval;

	//只在程式內部使用的參數
	/// <summary>
	/// 當前計算間隔的時間，超過間隔後會歸零重算
	/// </summary>
	private float tickTieme;
	
	// Update is called once per frame
	void Update () {

		//計算間隔時間
		tickTieme += Time.deltaTime;
		if(tickTieme >= spawnInterval)
		{
			//當超過間隔時間，當前的計算時間會歸零重算，並且在隨機的生成點生成一個物件
			tickTieme = 0;
			int randomIndex = Random.Range(0, spawnPoints.Length);
			Vector3 spawnPosition = spawnPoints[randomIndex].position;
			Quaternion spawnRotation = spawnPoints[randomIndex].rotation;
			Instantiate(spawnPrefab, spawnPosition, spawnRotation);
		}
	}
}
