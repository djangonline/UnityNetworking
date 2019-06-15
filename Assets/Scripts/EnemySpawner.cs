using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    public GameObject enemyPrefab;
    public int totalEnemies;

    public override void OnStartServer()
    {
        for(int i =0; i<totalEnemies; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8.0f, 8.0f), 0.0f, Random.Range(-8.0f, 8.0f));
            Quaternion spawnRot = Quaternion.Euler(0.0f, Random.Range(0,180), 0.0f);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, spawnRot) as GameObject;
            NetworkServer.Spawn(enemy);
        }
    }

}
