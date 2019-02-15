using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public Transform target;

    public GameObject Effect;

    Vector3 vector;

    private void Start()
    {
        if (GetComponentInParent<Ai>().target != null)
        {
            target = GetComponentInParent<Ai>().target;
        }
    }

    public void Update()
    {
        if (target != null)
        {
            if ((target.transform.position.x - transform.position.x) > 0)
                vector = new Vector3(0, 1, 0);
            else
                vector = new Vector3(0, -1, 0);            
            transform.Translate(vector * 5f * Time.deltaTime);
        }
        else if (target == null)
        {
            transform.Translate(transform.right * 5f * Time.deltaTime);
        }

        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Info>().Hp -= 10;
            Debug.Log(other.gameObject.name + " " + other.GetComponent<Info>().Hp);
            
            GameObject FbEffect = Instantiate(Effect, gameObject.transform.position, Quaternion.identity) as GameObject;

            Destroy(FbEffect, 1f);
            Destroy(gameObject);

        }
    }   
}
