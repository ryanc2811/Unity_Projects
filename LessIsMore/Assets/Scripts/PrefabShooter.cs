using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabShooter : Weapon
{
    //DECLARE a Transform for storing a reference to the point at which the bullet will be fired from
    public Transform firePoint;
    //DECLARE a GameObject for storing the bullet prefab
    public GameObject bulletPrefab;
    public int maxAmmoCount=10;
    public int ammoCount;
    public float bulletForce = 20f;


    // Start is called before the first frame update
    void Start()
    {
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammoCount > 0)
            {
                Shoot();
            }
            
        }
        if (ammoCount== 0)
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    void Reload()
    {
        ammoCount = maxAmmoCount;
        EventManager.instance.AmmoChangedTrigger(AmmoPercent());
    }
    float AmmoPercent()
    {
        float ammoPercent = (float)ammoCount / (float)maxAmmoCount;
        return ammoPercent;
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        ammoCount--;
        EventManager.instance.AmmoChangedTrigger(AmmoPercent());
    }
}
