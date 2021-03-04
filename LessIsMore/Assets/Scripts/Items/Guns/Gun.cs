using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using System;

public class Gun : MonoBehaviour
{
    [Header("Bullet sprite")]
    public GameObject projectile;

    [Header("Gun attributes")]
    public float force;
    public float damage;
    public int shots = 0;
    public int magazine = 20;
    public int reserve=999999999;
    public float reloadSpeed=1f;
    private float lastTimeShot=0;
    public float minTimeBetweenShots = .2f;
    private bool reloading = false;
    public float recoil=0;
    [Header("Vectors")]
    public Transform startPoint;
    Vector2 mousePos;
    [Header("Animator")]
    public Animator animator;
    [Header("UI")]
    public Text AmmoCount;
    public void Start()
    {
        shots = magazine;
        
    }
    /// <summary>
    /// SHOOTS THE GUN
    /// </summary>
    void Shoot()
    {
        Vector2 position = transform.position;
        EventManager.instance.GunShotTrigger(recoil);
        if (mousePos.x > position.x)
            {
                GameObject bullet = Instantiate(projectile, startPoint.position, Quaternion.identity);

                Vector2 direction = (mousePos - position).normalized;

                bullet.GetComponent<Rigidbody2D>().velocity = direction * force;
                bullet.GetComponent<Bullet>().damage = damage;
                shots--;
                lastTimeShot = Time.time;
            }

            else if (mousePos.x < position.x)
            {
                GameObject bullet = Instantiate(projectile, startPoint.position, Quaternion.identity);

                Vector2 direction = (mousePos - position).normalized;
                Vector3 BulletScale = bullet.transform.localScale;
                BulletScale.x *= -1;
                bullet.transform.localScale = BulletScale;
                bullet.GetComponent<Rigidbody2D>().velocity = direction * force;
                bullet.GetComponent<Bullet>().damage = damage;
                shots--;
                lastTimeShot = Time.time;
        }

        }
    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetButton("Fire2") && CanShoot())
            if(lastTimeShot + minTimeBetweenShots < Time.time)
                Shoot();

        if (Input.GetKeyDown(KeyCode.R) && CanReload())
            StartCoroutine("Reload");
            

        AmmoCount.text = shots + " / " + reserve;
    }
    bool CanReload()
    {
        if(!reloading && shots != magazine && reserve>0)
        {
            return true;
        }
        return false;
    }
    bool CanShoot()
    {
        if (shots > 0&& !reloading)
            return true;
        
        return false;
    }
    IEnumerator Reload()
    {
        reloading = true;
        //Play Reloading Sound
        yield return new WaitForSeconds(reloadSpeed);

        if (shots > 0)
        {
            reserve += shots;
        }
        if (reserve > magazine)
        {
            if (reserve - magazine >= 0)
            {
                shots = magazine;
                reserve -= magazine;
            }
        }
        else
        {
            shots = reserve;
            reserve = 0;
        }
        
        
        reloading = false;
    }
}
