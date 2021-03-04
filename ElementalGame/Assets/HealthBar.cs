using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private CurrentHealth currentHealth;
    private Quaternion rotation;
    private Transform parent;
    // Start is called before the first frame update
    public void Setup(CurrentHealth health)
    {
        currentHealth = health;
        parent = transform.parent;
        currentHealth.OnHealthChanged += CurrentHealth_OnHealthChanged;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(parent.position.x, parent.position.y, parent.position.z+5);
    }
    private void CurrentHealth_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("HealthBar").Find("Bar").GetComponent<Slider>().value = currentHealth.GetHealthPercent();
    }

}
