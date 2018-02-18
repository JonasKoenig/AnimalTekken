using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour {

    public float lifetime;
    public GameObject explosionPrefab;

    void Start()
    {
        //GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10f,10f),0f,0f),ForceMode.VelocityChange);
        Destroy(gameObject, lifetime);
    }

    public void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag=="Player"){
            PlayerMotor p = c.gameObject.GetComponent<PlayerMotor>();
                p.ResetPlayer();
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject, 2);
        Destroy(gameObject);
    }
}
