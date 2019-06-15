using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public Transform firePointTransform;
    public GameObject bulletGameObject;
    
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 3f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }    
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;

    }
    [Command]
    public void CmdFire()
    {
        GameObject bulletIns = (GameObject)Instantiate(bulletGameObject, firePointTransform.position, firePointTransform.rotation);
        bulletIns.GetComponent<Rigidbody>().velocity = transform.forward * 5f;

        NetworkServer.Spawn(bulletIns);

        Destroy(bulletIns, 3); ;
    }
}