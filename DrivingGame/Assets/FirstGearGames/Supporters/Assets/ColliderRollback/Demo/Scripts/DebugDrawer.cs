#if UNITY_EDITOR

using Mirror;
using UnityEngine;

namespace FirstGearGames.Mirrors.Assets.ColliderRollbacks.Demos
{

    /// <summary>
    /// For debugging only.
    /// </summary>
    public class DebugDrawer : MonoBehaviour
    {

        public static DebugDrawer Instance { get; private set; }

        [Range(0f, 0.35f)]
        [SerializeField]
        private float _rollbackTime = 0.2f;
        public static float RollbackTime
        {
            get
            {
                if (Instance == null)
                    return 0f;
                else
                    return Instance._rollbackTime;
            }
        }

        private void Awake()
        {
            Instance = this;
        }
        private float _nextPingTime = 0f;
        private void Update()
        {
            if (NetworkClient.active)
            {
                if (Time.time > _nextPingTime)
                { 
                    Debug.Log(NetworkTime.rtt * 1000f);
                    _nextPingTime = Time.time + 1f;
                }

            }
            if (_rollbackTime > 0f)
            {
                RollbackManager.Rollback(_rollbackTime, RollbackManager.PhysicsTypes.ThreeDimensional);
                //Perform raycast here.
                RollbackManager.ReturnForward();
            }
        }

        //private void ClientFire()
        //{
        //    double rollbackTime = NetworkTime.time - (NetworkTime.rtt / 2d);
        //    CmdFire(rollbackTime);
        //}

        //[Command]
        //private void CmdFire(double rollbackTime)
        //{
        //    float adjustedRollbackTime = (float)(NetworkTime.time - rollbackTime) - Time.fixedDeltaTime;
        //    RollbackManager.Rollback(adjustedRollbackTime, RollbackManager.PhysicsTypes.ThreeDimensional);
        //    //Perform raycast here.
        //    RollbackManager.ReturnForward();
        //}

    }

}
#endif