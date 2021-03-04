using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Joystick joystick;
    private GameObject bullet;
    public GameObject explosion;
    private bool bulletActive = false;
    Rigidbody bulletRB;
    Vector2 lastdirection;
    private bool waiting = false;
    float speed = 15f;
    private void Awake()
    {
        //create the instance of the joystick class
        joystick = new Joystick();
        //subscribe the move function to the joystick move callback
        joystick.OnJoystickMoveCallback += Move;
    }
    // Start is called before the first frame update
    private void Start()
    {
        
        EventManager.instance.OnNewBulletTrigger += SetBullet;
        EventManager.instance.OnBulletDestroyedTrigger += BulletDestroyed;
        
    }
    IEnumerator WaitToMove()
    {
        waiting = true;
        yield return new WaitForSeconds(.5f);
        waiting = false;
    }
    
    private void BulletDestroyed()
    {
        GameObject tempExplosion = Instantiate(explosion, bullet.transform.position, Quaternion.identity, null);
        Destroy(tempExplosion, 2f);
        Destroy(bullet);
        bulletActive = false;
    }
    private void SetBullet(GameObject bullet,Vector2 direction)
    {
        this.bullet = bullet;
        bulletRB = bullet.GetComponent<Rigidbody>();

        bulletActive = true;
        StartCoroutine(WaitToMove());
        lastdirection = direction;
    }
    private void Move(Vector2 direction)
    {
        if (direction != Vector2.zero&& !waiting)
            lastdirection = direction;
    }
    private void FixedUpdate()
    {
        if (bulletActive&&!waiting)
        {
            bulletRB.velocity=(new Vector3(-lastdirection.x, 0f, -lastdirection.y) * speed);
        }
        if(bulletActive)
            bulletRB.transform.LookAt(bulletRB.transform.position + -bulletRB.velocity);
        if (GameState.instance.isPaused)
        {
            lastdirection = Vector2.zero;
            transform.position = transform.position;
            return;
        }
    }
    void OnDestroy()
    {
        EventManager.instance.OnNewBulletTrigger -= SetBullet;
        EventManager.instance.OnBulletDestroyedTrigger -= BulletDestroyed;
    }
}
