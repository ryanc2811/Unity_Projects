﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovement
{
    void MoveToRelic();
    bool ReachedRelic();
}