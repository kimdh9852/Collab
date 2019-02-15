using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Transform target;
    public Info parent;

    public float gravity;
    public float speed;
    public float target_angle;
    public float arrow_angle;
    public float distance;

    void Start()
    {
        if (GetComponentInParent<Ai>().target != null)
        {
            parent = GetComponentInParent<Info>();
            target = GetComponentInParent<Ai>().target;
            target_angle = GetDegree(transform.position, target.transform.position);
            distance = Vector3.Distance(target.transform.position, transform.position);
            arrow_angle = Mathf.Asin(distance * gravity / (speed * speed) * Mathf.PI / 180.0f) / 2f;
        }
    }

    void Update()
    {

        if (target != null)
        {
            transform.position += new Vector3(speed * Mathf.Cos(arrow_angle) - gravity * Time.deltaTime,
                speed * Mathf.Sin(arrow_angle), 0) * Time.deltaTime;
        }
        else if (target == null)
        {
            transform.Translate(transform.up * 8 * Time.deltaTime);
        }

        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        float damage = 0;
        if (other.tag == "Enemy")
        {
            if (Random.Range(1, 1000) > target.GetComponent<Info>().Evade * 10)
            {
                if (Random.Range(1, 1000) < parent.Critcal * 10)
                {
                    damage = parent.Power * parent.CD - target.GetComponent<Info>().PhyArmor;
                }
                else
                {
                    damage = parent.Power - target.GetComponent<Info>().PhyArmor;
                }

                target.gameObject.GetComponent<Info>().Hp -= damage;

                Debug.Log(target.gameObject.name + " / " + target.gameObject.GetComponent<Info>().Hp);
            }
            else
            {
                Debug.Log("적 회피!");
            }
            Debug.Log(other.gameObject.name + " " + other.GetComponent<Info>().Hp);
            Destroy(gameObject);
        }
    }

    float GetDegree(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.y - _from.y, _to.x - _from.x);
    }
}
