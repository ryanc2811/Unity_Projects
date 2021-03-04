using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    string AnimationKey { get; }
    bool IsActive { get; set; }
    void FinishAttack();
    void CheckAttackHitBox();
}
