using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private CharacterController2D chrctrl;
    public Gun gun;
    private IDictionary<string, float> gunOffsets;
    private Vector2 startAimPos;
    Vector3 aimDirection;
    private void Awake()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        chrctrl = g.GetComponent<CharacterController2D>();

        aimTransform = transform.Find("Aim");
        gunOffsets = new Dictionary<string, float>()
        {
            {"Down", -0.5f},
            {"Left", -0.5f},
            {"Right", 0.5f}
        };
    }

    void Start()
    {
        EventManager.instance.OnGunShotTrigger += OnGunShotTrigger;
        
    }

    private void OnGunShotTrigger(float recoil)
    {
        chrctrl.RB.AddForce(-aimDirection * recoil,ForceMode2D.Force);
    }
    /// <summary>
    /// AIMS THE WEAPON
    /// </summary>
    void Aim()
    {
        //if (PlayerLoadout.Instance.Loadout[0] != null) {
        //    GameObject d = PlayerLoadout.Instance.Loadout[0].transform.gameObject;
        //    gun = d.gameObject.GetComponentInChildren<Gun>();
        //    Renderer gunRenderer = d.GetComponent<Renderer>();
       
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    aimDirection = (mousePos - transform.position).normalized;
        //    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //    aimTransform.eulerAngles = new Vector3(0, 0, angle);

        //    Vector2 playerPos = chrctrl.transform.position;

        //    if (aimDirection.x > 0)
        //    {
        //        aimTransform.position = new Vector3(playerPos.x+gunOffsets["Right"],playerPos.y,-1);
        //    }
        //    else
        //    {
        //        aimTransform.position = new Vector3(playerPos.x + gunOffsets["Left"],playerPos.y,-1);
        //    }
        //    if (aimDirection.y < -0.2)
        //    {
        //        aimTransform.position = new Vector3(playerPos.x, playerPos.y + gunOffsets["Down"], -1);
        //    }
        //    if (angle > 90 || angle < -90)
        //    {
        //        d.transform.rotation = Quaternion.Euler(-180, 0, -angle);
                
        //    }
        //    else
        //        d.transform.rotation = Quaternion.Euler(0, 0, angle);

        //        if (chrctrl.direction.y > 0.2 && chrctrl.direction.y <= 1)
        //            gunRenderer.sortingOrder = chrctrl.gameObject.GetComponent<Renderer>().sortingOrder - 1;
        //        else
        //            gunRenderer.sortingOrder = chrctrl.gameObject.GetComponent<Renderer>().sortingOrder +1;
        //}
    }
  
    private void Update()
    {

        //if (!UIHandler.Instance.IsMouseOverIgnoredUI())
            Aim();
        //else
        //    return;
    }
}
