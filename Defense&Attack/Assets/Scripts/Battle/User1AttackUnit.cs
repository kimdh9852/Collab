using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User1AttackUnit : MonoBehaviour
{
    //길찾아가기만 설정하여 추후 상대 병사를 만났을 때 싸우는거 코딩 덧붙여야함..
    public float speed = 1f;
    private Transform waytarget;
    private Transform enemytarget;
    private int wavepointIndex = 0;

    //범위
    public float range = 2f;
    //공속
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public Transform partToRotate;
    public float turnSpeed = 3f;

    //총알 프리팹
    public GameObject bulletPrefab;
    //발사 위치 
    public Transform firePoint;

    //public GameObject destoryeffect;

    public string enemyTag = "User2Unit";

    //내비게이션에 사용될 point들을 모아둠
    void Start()
    {
        waytarget = Waypoints.points[0];
        InvokeRepeating("UpdateTarget", 1.0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            enemytarget = nearestEnemy.transform;
        }
        else
        {
            enemytarget = null;
            //Vector3 dir = waytarget.position - transform.position;
            //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            //if (Vector3.Distance(transform.position, waytarget.position) <= 0.4f)
            //{
            //    GetNextWaypoint();
            //}
        }
    }

    void Update()
    {
        Vector3 dir = waytarget.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime * 0.5f, Space.World);
        if (Vector3.Distance(transform.position, waytarget.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        if (fireCountdown <= 0f)
        {
            StartCoroutine(Shoot());
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    //상대방 공격
    IEnumerator Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(enemytarget);

        yield return new WaitForSeconds(0.2f);
    }

    // 다음 point로 이동 어떻게 멈추지..?
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            return;
        }
        wavepointIndex++;
        waytarget = Waypoints.points[wavepointIndex];
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
