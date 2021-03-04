using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //reference to the player aim class
    PlayerAim playerAim;
    //prefab for the bullet object
    [SerializeField]
    private GameObject bulletPrefab;
    //amount of bullet the player can fire in the level
    [SerializeField]
    private int ammoCount = 3;
    private bool ammoCountSet = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerAim = GetComponent<PlayerAim>();
        playerAim.OnFireGunCallback += Fire;
    }
    void Update()
    {
        if (!ammoCountSet)
        {
            ammoCountSet = true;
            EventManager.instance.AmmoCountSetTrigger(ammoCount);
        }
    }
    void Fire(Vector3 direction)
    {
        
        if (ammoCount > 0)
        {
            EventManager.instance.AmmoSpentTrigger();
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);
            EventManager.instance.NewBulletTrigger(bullet,direction);
            ammoCount--;
            bullet.GetComponent<Rigidbody>().velocity = -direction * 15f;
        }
    }
    void OnDestroy()
    {
        playerAim.OnFireGunCallback -= Fire;
    }
}
