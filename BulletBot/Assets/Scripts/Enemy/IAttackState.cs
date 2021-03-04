using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackState
{
    void SetTarget(Transform target);
    void SetCurrentTransform(Transform attacker);
}
