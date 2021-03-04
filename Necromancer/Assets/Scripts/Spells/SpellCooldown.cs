using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour
{
    public Image maskImage;
    [SerializeField]
    private Spell spell;
    [SerializeField]
    private GameObject weaponHolder;
    private Image buttonImage;
    private AudioSource audioSource;
    private float spellCooldown, nextReadyTime, coolDownTimeLeft;
    private bool isSpellReady = false;
    private bool isSpellSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize(spell, weaponHolder);
        EventManager.instance.OnDoubleClickTrigger += DoubleClick;
        buttonImage = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void Initialize(Spell selectedSpell,GameObject weaponHolder)
    {
        spell = selectedSpell;
        buttonImage.enabled = true;
        buttonImage.sprite = spell.image;
        maskImage.sprite = spell.image;
        spellCooldown = spell.cooldown;
        spell.Initialize(weaponHolder);
        SpellReady();
    }
    private void SpellReady()
    {
        maskImage.enabled = false;
    }
    public void Reset()
    {
        spell = null;
        buttonImage.enabled = false;
        //audioSource.enabled = false;
        maskImage.enabled = false;
        spellCooldown = 0;
        nextReadyTime = 0;
        coolDownTimeLeft = 0;
        isSpellReady = false;
        isSpellSelected = false;
        gameObject.SetActive(false);
    }
    public void SpellSelected(bool selected)
    {
        isSpellSelected =selected;
    }
    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        maskImage.fillAmount = (coolDownTimeLeft / spellCooldown);
    }
    // Update is called once per frame
    void Update()
    {
        isSpellReady = (Time.time > nextReadyTime);
        if (isSpellReady)
        {
            SpellReady();
        }
        else
        {
            CoolDown();
        }
    }
    private void DoubleClick(Vector3 position)
    {
        if (isSpellReady&&isSpellSelected)
        {
            nextReadyTime = spellCooldown + Time.time;
            coolDownTimeLeft = spellCooldown;
            maskImage.enabled = true;
            //audioSource.clip = spell.sound;
            //audioSource.Play();
            spell.TriggerAbility(position);
        }
    }
    void OnDestroy()
    {
        EventManager.instance.OnDoubleClickTrigger -= DoubleClick;
    }
}
