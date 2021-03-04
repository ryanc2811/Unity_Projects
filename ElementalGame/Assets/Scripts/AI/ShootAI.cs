using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAI : MonoBehaviour,IAIAttack
{
    public GameObject attackPrefab;
    public void Attack(GameObject target, int damage)
    {
        Vector3 heading = transform.position - target.transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        transform.rotation = Quaternion.LookRotation(direction);

        GameObject tempAttack = Instantiate(attackPrefab, transform.position, transform.rotation, null);
        if (gameObject.CompareTag("Enemy"))
            tempAttack.GetComponent<EnemyBullet>().Initialize(damage);
        tempAttack.GetComponent<Rigidbody>().AddForce(-direction * 30f, ForceMode.Impulse);
    }
}
