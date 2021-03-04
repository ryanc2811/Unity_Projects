using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public event Action<GameObject> OnAIDeathTrigger;
    public event Action <GameObject>OnPlayerDeathTrigger;
    public event Action<GameObject> OnNewPlayerTrigger;
    public event Action OnLevelEndedTrigger;
    public event Action<Vector3> OnDoubleClickTrigger;
    public event Action OnSpellChangedTrigger;
    public event Action <GameObject>OnSpellSelectedTrigger;
    public event Action<Trait> OnTraitAddedTrigger;
    public event Action<Trait> OnTraitChosenTrigger;
    public event Action<Trait> OnTraitRemovedTrigger;
    public event Action OnExperienceChangedTrigger;
    public event Action OnLevelChangedTrigger;
    public event Action<int> OnGivePlayerExperienceTrigger;
    public event Action<float> OnHealPlayerTrigger;

    public void HealPlayerTrigger(float health)
    {
        if (OnHealPlayerTrigger != null)
        {
            OnHealPlayerTrigger(health);
        }
    }
    public void GivePlayerExperienceTrigger(int xp)
    {
        if (OnGivePlayerExperienceTrigger != null)
        {
            OnGivePlayerExperienceTrigger(xp);
        }
    }
    public void LevelChangedTrigger()
    {
        if (OnLevelChangedTrigger != null)
        {
            OnLevelChangedTrigger();
        }
    }
    public void ExperienceChangedTrigger()
    {
        if (OnExperienceChangedTrigger != null)
        {
            OnExperienceChangedTrigger();
        }
    }
    public void TraitChosenTrigger(Trait trait)
    {
        if (OnTraitChosenTrigger != null)
        {
            OnTraitChosenTrigger(trait);
        }
    }
    public void TraitAddedTrigger(Trait trait)
    {
        if (OnTraitAddedTrigger != null)
        {
            OnTraitAddedTrigger(trait);
        }
    }
    public void TraitRemovedTrigger(Trait trait)
    {
        if (OnTraitRemovedTrigger != null)
        {
            OnTraitRemovedTrigger(trait);
        }
    }
    public void SpellSelectedTrigger(GameObject spell)
    {
        if (OnSpellSelectedTrigger != null)
        {
            OnSpellSelectedTrigger(spell);
        }
    }
    public void SpellChangedTrigger()
    {
        if (OnSpellChangedTrigger != null)
        {
            OnSpellChangedTrigger();
        }
    }
    public void DoubleClickTrigger(Vector3 position)
    {
        if (OnDoubleClickTrigger != null)
        {
            OnDoubleClickTrigger(position);
        }
    }
    public void LevelEndedTrigger()
    {
        if (OnLevelEndedTrigger != null)
        {
            OnLevelEndedTrigger();
        }
    }
    public void AIDeathTrigger(GameObject GO)
    {
        if (OnAIDeathTrigger != null)
        {
            OnAIDeathTrigger(GO);
        }
    }
    public void PlayerDeathTrigger(GameObject player)
    {
        if (OnPlayerDeathTrigger != null)
        {
            OnPlayerDeathTrigger(player);
        }
    }
    public void NewPlayerTrigger(GameObject player)
    {
        if (OnNewPlayerTrigger != null)
        {
            OnNewPlayerTrigger(player);
        }
    }
}
