using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User1Turret : MonoBehaviour
{
    private Transform target;

    [Header("General")]
    //범위
    public float range = 3f;
    //공속
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "User1Unit";

    //머리 방향
    public Transform partToRotate;
    public float turnSpeed = 10f;

    //총알 프리팹
    public GameObject bulletPrefab;
    //발사 위치 
    public Transform firePoint;


    // 해당 시간이 지난 이후에 일정한 시간 간격으로 메서드를 반복해서 호출
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    //타겟을 발견, 적 유무 판단, 거리 확인
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
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // 적보면 몸?이 적을 향하게 함 
    void Update()
    {
        if (target == null)
            return;
        //Target Lock On
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            StartCoroutine(Shoot());
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    //적 공격
    IEnumerator Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);

        yield return new WaitForSeconds(0.2f);
    }

    //범위를 표시 하는 빨간 선 씬뷰에서만 보이고 게임 뷰에서는 보이지 않음 디버그용 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
