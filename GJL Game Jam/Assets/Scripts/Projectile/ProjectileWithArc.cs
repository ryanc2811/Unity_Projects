using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWithArc : MonoBehaviour,IProjectile
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public Quaternion AimRotation(Vector3 start, Vector3 end, float velocity)
    {

        float low;
        //float high;
        Ballistics.CalculateTrajectory(start, end, velocity, out low);//, out high); //get the angle


        Vector3 wantedRotationVector = Quaternion.LookRotation(end - start).eulerAngles; //get the direction
        wantedRotationVector.x = low; //combine the two
        return Quaternion.Euler(wantedRotationVector); //into a quaternion
    }

    public void Shoot(Transform target)
    {
        //Quaternion angle=AimRotation()
    }
}
