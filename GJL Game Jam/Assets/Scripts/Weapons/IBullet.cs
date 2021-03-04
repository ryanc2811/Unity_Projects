using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    void Fire(float xDirection, float bulletSpeed, float pDamage);
}
