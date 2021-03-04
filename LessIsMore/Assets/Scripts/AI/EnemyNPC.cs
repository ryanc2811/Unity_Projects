using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// Abstract class for all enemys
    /// </summary>
    abstract class EnemyNPC:MonoBehaviour
    {
        /// <summary>
        /// gets the type of enemy that the enemy is
        /// </summary>
        public abstract EnemyType EnemyType { get; }
    }
    public enum EnemyType
    {
        Normal,
        Boss
    }
}
