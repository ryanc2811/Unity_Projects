using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform armRotatePivot;
    public Transform playerTransform;
    private Rigidbody2D playerRB;
    public Transform gun;
    private float x;
    private Vector3 ls;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = playerTransform.GetComponent<Rigidbody2D>();
        x = armRotatePivot.localScale.x;
        ls = armRotatePivot.localScale;
    }
    void RotateGun(float angle)
    {
        if (angle > 90 || angle < -90)
        {
            gun.rotation = Quaternion.Euler(-180, 0, -angle);
        }
        else
            gun.rotation = Quaternion.Euler(0, 0, angle);
    }
    void AimWeapon()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        armRotatePivot.eulerAngles = new Vector3(0, 0, angle);

        if (playerRB.velocity == Vector2.zero)
        {
            if (aimDirection.x > 0)
                playerTransform.eulerAngles = new Vector3(0, 180, 0);
            

            else
                playerTransform.eulerAngles = new Vector3(0, 0, 0);

            RotateGun(angle);

        }
        else
        {
            RotateGun(angle);
        }

    }
    // Update is called once per frame
    void Update()
    {
        //AimWeapon();
    }
}
