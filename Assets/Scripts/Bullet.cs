using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter(Collision col)
    {
        var hit = col.gameObject;
        var health = hit.GetComponent<Health>();

        if(health != null)
        {
            health.TakeDamage(25);
        }

        Destroy(gameObject);
    }
}
