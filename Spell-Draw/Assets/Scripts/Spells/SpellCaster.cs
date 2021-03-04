using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    SpellLoadout spellLoadout;
    DragToAim dragToAim;
    DrawPad drawPad;
    // Start is called before the first frame update
    void Start()
    {
        spellLoadout = SpellLoadout.Instance;
        drawPad = DrawPad.instance;
        dragToAim = GetComponent<DragToAim>();
        dragToAim.OnSpellCastCallback += CastSpell;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CastSpell(Vector3 direction)
    {
        if (drawPad.gestureNameTxt.text != null)
        {
            Spell spellScript = spellLoadout.SpellInLoadout(drawPad.gestureNameTxt.text);
            if (spellScript)
            {
                GameObject spell=Instantiate(spellScript.prefab, transform.position, Quaternion.identity, null);
               spell.GetComponent<SpellEffect>().StartEffect(direction);
            }
        }
    }
}
