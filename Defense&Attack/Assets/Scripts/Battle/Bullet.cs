using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed = 30f;

    //추후 이펙트가 나오면 추가할 것 
    //public GameObject impactEffect;
    //public GameObject destoryEffect;

    // 타겟을 찾는다
    public void Seek(Transform _target)
    {
        target = _target;
    }

    //총알이 어떻게 날아가는지
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;

        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    // 총알이 타겟에 맞았을 때... 현재는 한방이기 때문에 맞으면 맞은 대상은 바로 제거가 됨.
    void HitTarget()
    {
        //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 1f);
        //Destroy(target.gameObject);

        //GameObject destroyEffect = (GameObject)Instantiate(destoryEffect, transform.position, transform.rotation);
        //Destroy(destroyEffect, 1f);
        if (target.gameObject.tag == "User2Unit")
        {
            if (target.gameObject.name == "Base")
            {
                Base.HP -= 1;
            }
            else
            {
                Destroy(target.gameObject);
            }
        }

        Destroy(gameObject);
    }
}
