using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMorphController
{
    Action<MorphType, MorphType, float> MorphStateChangeRequest { get; set; }
    CurrentHealth ReceiveDamage { get; }
}
