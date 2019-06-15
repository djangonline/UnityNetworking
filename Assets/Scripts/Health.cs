using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 200;
    [SyncVar(hook = "OnchangeHealth")]
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    public bool destroyOnDeath;

    NetworkStartPosition[] spawnPoints;

    private void Start()
    {
        if (isLocalPlayer)
            spawnPoints = FindObjectsOfType<NetworkStartPosition>(); //onemli
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isServer)
            return;

        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;
                RpcRespawn();
            }
            
        }
    }

    void OnchangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;
            
            if(spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }
            transform.position = spawnPoint;
        }
        
    }
}
