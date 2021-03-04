using FirstGearGames.Utilities.Editors;
using FirstGearGames.Utilities.Maths;
using FirstGearGames.Utilities.Objects;
using Mirror;
using UnityEngine;

namespace FirstGearGames.Mirrors.Assets.FlexNetworkTransforms
{


    public abstract class FlexNetworkTransformBase : NetworkBehaviour
    {
        #region Types.
        /// <summary>
        /// Used to smooth interpolation when data becomes unreliable.
        /// </summary>
        // implementation of N-day EMA
        // it calculates an exponential moving average roughy equivalent to the last n observations
        // https://en.wikipedia.org/wiki/Moving_average#Exponential_moving_average
        public class FloatExponentialMovingAverage
        {
            readonly float alpha;
            bool initialized;

            public FloatExponentialMovingAverage(int n)
            {
                // standard N-day EMA alpha calculation
                alpha = 2.0f / (n + 1);
            }

            public void Add(float newValue)
            {
                // simple algorithm for EMA described here:
                // https://en.wikipedia.org/wiki/Moving_average#Exponentially_weighted_moving_variance_and_standard_deviation
                if (initialized)
                {
                    float delta = newValue - Value;
                    Value += alpha * delta;
                    //Var = (1 - alpha) * (Var + alpha * delta * delta);
                }
                else
                {
                    Value = newValue;
                    initialized = true;
                }
            }

            public float Value { get; private set; }

            //public float Var { get; private set; }
        }

        private struct MoveRate
        {
            public float Position;
            public float Rotation;
            public float Scale;
        }
        //EXTRAPOLATION
        ///// <summary>
        ///// Types of extrapolation durations.
        ///// </summary>
        //private enum ExtrapolationDurations
        //{
        //    Off = 0,
        //    VeryShort = 1,
        //    Short = 2,
        //    Medium = 3,
        //    Long = 4
        //}
        /// <summary>
        /// Data used to manage moving towards a target.
        /// </summary>
        private class TargetSyncData
        {
            public TargetSyncData(TransformSyncData goalData, float receivedTime)
            {
                GoalData = goalData;
                ReceivedTime = receivedTime;
            }

            /// <summary>
            /// Time data was received.
            /// </summary>
            public readonly float ReceivedTime;
            /// <summary>
            /// Transform goal data for this update.
            /// </summary>
            public TransformSyncData GoalData;
        }
        /// <summary>
        /// Ways to synchronize datas.
        /// </summary>
        [System.Serializable]
        private enum SynchronizeTypes : int
        {
            Normal = 0,
            NoSynchronization = 1
        }
        /// <summary>
        /// Interval types to determine when to synchronize data.
        /// </summary>
        [System.Serializable]
        private enum IntervalTypes : int
        {
            Timed = 0,
            FixedUpdate = 1
        }
        #endregion

        #region Protected.
        /// <summary>
        /// Transform to monitor and modify.
        /// </summary>
        protected abstract Transform TargetTransform { get; }
        #endregion

        #region Serialized.
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("True to synchronize using localSpace rather than worldSpace. If you are to child this object throughout it's lifespan using worldspace is recommended. However, when using worldspace synchronization may not behave properly on VR. LocalSpace is the default.")]
        [SerializeField]
        private bool _useLocalSpace = true;
        /// <summary>
        /// How to operate synchronization timings. Timed will synchronized every specified interval while FixedUpdate will synchronize every FixedUpdate.
        /// </summary>
        [Tooltip("How to operate synchronization timings. Timed will synchronized every specified interval while FixedUpdate will synchronize every FixedUpdate.")]
        [SerializeField]
        private IntervalTypes _intervalType = IntervalTypes.Timed;
        /// <summary>
        /// How often to synchronize this transform.
        /// </summary>
        [Tooltip("How often to synchronize this transform.")]
        [Range(0.01f, 0.5f)]
        [SerializeField]
        private float _synchronizeInterval = 0.1f;
        /// <summary>
        /// True to synchronize using the reliable channel. False to synchronize using the unreliable channel. Your project must use 0 as reliable, and 1 as unreliable for this to function properly. This feature is not supported on TCP transports.
        /// </summary>
        [Tooltip("True to synchronize using the reliable channel. False to synchronize using the unreliable channel. Your project must use 0 as reliable, and 1 as unreliable for this to function properly.")]
        [SerializeField]
        private bool _reliable = true;
        /// <summary>
        /// True to synchronize data anytime it has changed. False to allow greater differences before synchronizing.
        /// </summary>
        [Tooltip("True to synchronize data anytime it has changed. False to allow greater differences before synchronizing.")]
        [SerializeField]
        private bool _preciseSynchronization = false;
        //EXTRAPOLATION
        ///// <summary>
        ///// How long to extrapolate transform for. Useful to improve fluid movement for players with bad connections. May want to use Short or Off for twitch response games, such as shooters.
        ///// Currently only position is extrapolated and client authoritative movement is not extrapolated on the server.
        ///// </summary>
        //[Tooltip("How long to extrapolate transform for. Useful to improve fluid movement for players with bad connections. May want to use VeryShort or Off for twitch response games, such as shooters.")]
        //[SerializeField]
        //private ExtrapolationDurations _extrapolationDuration = ExtrapolationDurations.VeryShort;
        /// <summary>
        /// True to automatically determine interpolation strength. False to specify your own value. Interpolation is used to reduce stutter for unreliable connections.
        /// </summary>
        [Tooltip("True to automatically determine interpolation strength. False to specify your own value. Interpolation is used to reduce stutter for unreliable connections.")]
        [SerializeField]
        private bool _automaticInterpolation = true;
        /// <summary>
        /// How strongly to interpolate to server results. Higher values will result in more real-time results but may result in occasional stutter during network instability.
        /// </summary>
        [Tooltip("How strongly to interpolate to server results. Higher values will result in more real-time results but may result in occasional stutter during network instability.")]
        [Range(0.1f, 1f)]
        [SerializeField]
        private float _interpolationStrength = 0.85f;
        /// <summary>
        /// True if using client authoritative movement.
        /// </summary>
        [Tooltip("True if using client authoritative movement.")]
        [SerializeField]
        private bool _clientAuthoritative = true;
        /// <summary>
        /// True to synchronize server results back to owner. Typically used when you are sending inputs to the server and are relying on the server response to move the transform.
        /// </summary>
        [Tooltip("True to synchronize server results back to owner. Typically used when you are sending inputs to the server and are relying on the server response to move the transform.")]
        [SerializeField]
        private bool _synchronizeToOwner = true;
        /// <summary>
        /// Synchronize options for position.
        /// </summary>
        [Tooltip("Synchronize options for position.")]
        [SerializeField]
        private SynchronizeTypes _synchronizePosition = SynchronizeTypes.Normal;
        /// <summary>
        /// Euler axes on the position to snap into place rather than move towards over time.
        /// </summary>
        [Tooltip("Euler axes on the rotation to snap into place rather than move towards over time.")]
        [SerializeField]
        [BitMask(typeof(Axes))]
        private Axes _snapPosition = (Axes)0;
        /// <summary>
        /// Sets SnapPosition value. For internal use only. Must be public for editor script.
        /// </summary>
        /// <param name="value"></param>
        public void SetSnapPosition(Axes value) { _snapPosition = value; }
        /// <summary>
        /// Synchronize states for rotation.
        /// </summary>
        [Tooltip("Synchronize states for position.")]
        [SerializeField]
        private SynchronizeTypes _synchronizeRotation = SynchronizeTypes.Normal;
        /// <summary>
        /// Euler axes on the rotation to snap into place rather than move towards over time.
        /// </summary>
        [Tooltip("Euler axes on the rotation to snap into place rather than move towards over time.")]
        [SerializeField]
        [BitMask(typeof(Axes))]
        private Axes _snapRotation = (Axes)0;
        /// <summary>
        /// Sets SnapRotation value. For internal use only. Must be public for editor script.
        /// </summary>
        /// <param name="value"></param>
        public void SetSnapRotation(Axes value) { _snapRotation = value; }
        /// <summary>
        /// Synchronize states for scale.
        /// </summary>
        [Tooltip("Synchronize states for scale.")]
        [SerializeField]
        private SynchronizeTypes _synchronizeScale = SynchronizeTypes.Normal;
        /// <summary>
        /// Euler axes on the scale to snap into place rather than move towards over time.
        /// </summary>
        [Tooltip("Euler axes on the scale to snap into place rather than move towards over time.")]
        [SerializeField]
        [BitMask(typeof(Axes))]
        private Axes _snapScale = (Axes)0;
        /// <summary>
        /// Sets SnapScale value. For internal use only. Must be public for editor script.
        /// </summary>
        /// <param name="value"></param>
        public void SetSnapScale(Axes value) { _snapScale = value; }
        #endregion

        #region Private.
        /// <summary>
        /// Last SyncData sent by client.
        /// </summary>
        private TransformSyncData _clientSyncData = null;
        /// <summary>
        /// Last SyncData sent by server.
        /// </summary>
        private TransformSyncData _serverSyncData = null;
        /// <summary>
        /// Next time client may send data.
        /// </summary>
        private float _nextClientSendTime = 0f;
        /// <summary>
        /// Last SequenceId received from client.
        /// </summary>
        private double _lastClientSequenceId = -1;
        /// <summary>
        /// Next time server may send data.
        /// </summary>
        private float _nextServerSendTime = 0f;
        /// <summary>
        /// Last SequenceId received from server.
        /// </summary>
        private double _lastServerSequenceId = -1;

        /// <summary>
        /// Average data arrival deviance for server received data.
        /// </summary>
        private FloatExponentialMovingAverage _serverDataDevianceInterpolationMultiplier = new FloatExponentialMovingAverage(10);
        /// <summary>
        /// TargetSyncData to move between.
        /// </summary>
        private TargetSyncData _targetSyncData = null;
        /// <summary>
        /// When sending data from client, after the transform stops changing and when using unreliable this becomes true while a reliable packet is being sent.
        /// </summary>
        private bool _clientSettleSent = false;
        /// <summary>
        /// When sending data from server, after the transform stops changing and when using unreliable this becomes true while a reliable packet is being sent.
        /// </summary>
        private bool _serverSettleSent = false;
        /// <summary>
        /// Last frame FixedUpdate ran.
        /// </summary>
        private int _lastFixedFrame = -1;
        /// <summary>
        /// Multiplier to apply towards the synchronization interval for time expected to achieve 100% lerp from Start to Goal on SyncData.
        /// </summary>
        private float _interpolationIntervalMultiplier;
        /// <summary>
        /// Current move rates.
        /// </summary>
        private MoveRate _moveRates = new MoveRate();
        #endregion

        private void Awake()
        {
            _interpolationIntervalMultiplier = ReturnInterpolationIntervalMultiplier();
            _serverDataDevianceInterpolationMultiplier = new FloatExponentialMovingAverage(Mathf.CeilToInt(2f / ReturnSyncInterval()));
        }

        private void Update()
        {
            CheckSendToServer();
            CheckSendToClients();
            MoveTowardsTargetSyncData();
        }

        private void FixedUpdate()
        {
            /* Don't send if the same frame. Since
             * physics aren't actually involved there is
             * no reason to run logic twice on the
             * same frame; that will only hurt performance
             * and the network more. */
            if (Time.frameCount == _lastFixedFrame)
                return;
            _lastFixedFrame = Time.frameCount;

            CheckSendToServer();
            CheckSendToClients();
        }

        /// <summary>
        /// Returns interpolation multiplier to use based on settings.
        /// </summary>
        private float ReturnInterpolationIntervalMultiplier()
        {
            float interval = ReturnSyncInterval();
            float multiplier;
            //Automatically calculate interpolation strength.
            if (_automaticInterpolation)
            {
                float lerpPercent = Mathf.InverseLerp(0.1f, 0f, interval);
                multiplier = Mathf.Lerp(0.95f, 0.6f, lerpPercent);
            }
            //Use configured value.
            else
            {
                multiplier = _interpolationStrength;
            }

            return multiplier;


            //float interval = ReturnSyncInterval();
            //float multiplier;
            ////Automatically calculate interpolation strength.
            //if (_automaticInterpolation)
            //{
            //    if (interval <= 0.1f)
            //    {
            //        float lerpPercent = Mathf.InverseLerp(0.1f, 0f, interval);
            //        multiplier = Mathf.Lerp(1.5f, 7f, lerpPercent);
            //    }
            //    //Higher than average sync intervals.
            //    else
            //    {
            //        float lerpPercent = Mathf.InverseLerp(0.5f, 0.1f, interval);
            //        multiplier = Mathf.Lerp(1.25f, 1.5f, lerpPercent);
            //    }
            //}
            ////Use configured value.
            //else
            //{
            //    multiplier = Mathf.Lerp(5f, 1f, _interpolationStrength);
            //}

            //return multiplier;
        }

        /// <summary>
        /// Returns synchronization interval used.
        /// </summary>
        /// <returns></returns>
        private float ReturnSyncInterval()
        {
            return (_intervalType == IntervalTypes.FixedUpdate) ? Time.fixedDeltaTime : _synchronizeInterval;
        }

        /// <summary>
        /// Checks if client needs to send data to server.
        /// </summary>
        private void CheckSendToServer()
        {
            //Timed interval.
            if (_intervalType == IntervalTypes.Timed)
            {
                if (Time.inFixedTimeStep)
                    return;

                if (Time.time < _nextClientSendTime)
                    return;
            }
            //Fixed interval.
            else
            {
                if (!Time.inFixedTimeStep)
                    return;
            }

            //Not using client auth movement.
            if (!_clientAuthoritative)
                return;
            //Only send to server if client.
            if (!base.isClient)
                return;
            //Not authoritative client.
            if (!base.hasAuthority)
                return;

            SyncProperties sp = ReturnDifferentProperties(_clientSyncData);

            bool useReliable = _reliable;
            if (!CanSendProperties(ref sp, ref _clientSettleSent, ref useReliable))
                return;
            //Add additional sync properties.
            ApplyRequiredSyncProperties(ref sp);

            /* This only applies if using interval but
             * add anyway since the math operation is fast. */
            _nextClientSendTime = Time.time + _synchronizeInterval;
            _clientSyncData = new TransformSyncData((byte)sp, NetworkTime.time,
                TargetTransform.GetPosition(_useLocalSpace), TargetTransform.GetRotation(_useLocalSpace), TargetTransform.GetScale());

            //send to clients.
            if (useReliable)
                CmdSendSyncDataReliable(_clientSyncData);
            else
                CmdSendSyncDataUnreliable(_clientSyncData);
        }

        /// <summary>
        /// Checks if server needs to send data to clients.
        /// </summary>
        private void CheckSendToClients()
        {
            //Timed interval.
            if (_intervalType == IntervalTypes.Timed)
            {
                if (Time.inFixedTimeStep)
                    return;

                if (Time.time < _nextServerSendTime)
                    return;
            }
            //Fixed interval.
            else
            {
                if (!Time.inFixedTimeStep)
                    return;
            }

            //Only send to clients if server.
            if (!base.isServer)
                return;

            SyncProperties sp = SyncProperties.None;
            /* If server only or has authority then use transforms current position.
             * When server only client values are set immediately, but as client host
             * they are smoothed so transforms do not snap. When smoothed instead of
             * sending the transforms current data we will send the goal data. This prevents
             * clients from receiving slower updates when running as a client host. */
            //Breaking if statements down for easier reading.
            if (base.isServerOnly || base.hasAuthority)
                sp = ReturnDifferentProperties(_serverSyncData);
            //No authority and not server only.
            else if (!base.hasAuthority && !base.isServerOnly)
                sp = ReturnDifferentProperties(_serverSyncData, _targetSyncData);

            bool useReliable = _reliable;
            if (!CanSendProperties(ref sp, ref _serverSettleSent, ref useReliable))
                return;
            //Add additional sync properties.
            ApplyRequiredSyncProperties(ref sp);

            /* This only applies if using interval but
            * add anyway since the math operation is fast. */
            _nextServerSendTime = Time.time + _synchronizeInterval;
            _serverSyncData = new TransformSyncData((byte)sp, NetworkTime.time,
                TargetTransform.GetPosition(_useLocalSpace), TargetTransform.GetRotation(_useLocalSpace), TargetTransform.GetScale());

            //send to clients.
            if (useReliable)
                RpcSendSyncDataReliable(_serverSyncData);
            else
                RpcSendSyncDataUnreliable(_serverSyncData);
        }


        /// <summary>
        /// Applies SyncProperties which are required based on settings.
        /// </summary>
        /// <param name="sp"></param>
        private void ApplyRequiredSyncProperties(ref SyncProperties sp)
        {
            //Extrapolation requires timestamps to help guestimate distance and speed.
            //EXTRAPOLATION
            //if (ReturnExtrapolationMultiplier() > 0f)
            //    sp |= SyncProperties.Sequenced;
            //If not reliable must send everything.
            if (!_reliable)
                sp |= (SyncProperties.Position | SyncProperties.Rotation | SyncProperties.Scale | SyncProperties.Sequenced);
            //If has settled then must include all transform values to ensure a perfect match.
            else if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Settled))
                sp |= (SyncProperties.Position | SyncProperties.Rotation | SyncProperties.Scale);
        }

        /// <summary>
        /// Returns if data updates should send based on SyncProperties, Reliable, and send history.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private bool CanSendProperties(ref SyncProperties sp, ref bool settleSent, ref bool useReliable)
        {
            //If nothing has changed.
            if (sp == SyncProperties.None)
            {
                /* If reliable is default and there's no extrapolation
                 * then there is no reason to send a settle packet.
                 * This is because extrapolation can overshoot while
                 * waiting for a new packet, but with extrapolation off
                 * the most recent reliable packet is always the latest
                 * data. */
                //EXTRAPOLATION.
                //if (_reliable && ReturnExtrapolationMultiplier() == 0f)
                //    return false;

                //Settle has already been sent.
                if (settleSent)
                {
                    return false;
                }
                //Settle has not been sent yet.
                else
                {
                    settleSent = true;
                    useReliable = true;
                    sp |= SyncProperties.Settled;
                    return true;
                }
            }
            //Properties need to be synchronized.
            else
            {
                //Unset settled.
                settleSent = false;

                return true;
            }

        }

        /// <summary>
        /// Returns which properties need to be sent to maintain synchronization with the transforms current properties.
        /// </summary>
        /// <returns></returns>
        private SyncProperties ReturnDifferentProperties(TransformSyncData data)
        {
            return ReturnDifferentProperties(data, null);
        }
        /// <summary>
        /// Returns which properties need to be sent to maintain synchronization with targetData properties.
        /// </summary>
        /// <returns></returns>
        private SyncProperties ReturnDifferentProperties(TransformSyncData data, TargetSyncData targetData)
        {
            //Data is null, so it's definitely not a match.
            if (data == null)
                return (SyncProperties.Position | SyncProperties.Rotation | SyncProperties.Scale);

            SyncProperties sp = SyncProperties.None;

            if (_synchronizePosition == SynchronizeTypes.Normal && !PositionMatches(data, targetData, _preciseSynchronization))
                sp |= SyncProperties.Position;
            if (_synchronizeRotation == SynchronizeTypes.Normal && !RotationMatches(data, targetData, _preciseSynchronization))
                sp |= SyncProperties.Rotation;
            if (_synchronizeScale == SynchronizeTypes.Normal && !ScaleMatches(data, targetData, _preciseSynchronization))
                sp |= SyncProperties.Scale;

            return sp;
        }

        /// <summary>
        /// Moves towards TargetSyncData.
        /// </summary>
        private void MoveTowardsTargetSyncData()
        {
            //No SyncData to check against.
            if (_targetSyncData == null)
                return;
            /* Client authority but there is no owner.
             * Can happen when client authority is ticked but
            * the server takes away authority. */
            if (base.isServer && _clientAuthoritative && base.connectionToClient == null && _targetSyncData != null)
            {
                /* Remove sync data so server no longer tries to sync up to last data received from client.
                 * Object may be moved around on server at this point. */
                _targetSyncData = null;
                return;
            }
            //Client authority, don't need to synchronize with self.
            if (base.hasAuthority && _clientAuthoritative)
                return;
            //Not client authority but also not synchronize to owner.
            if (base.hasAuthority && !_clientAuthoritative && !_synchronizeToOwner)
                return;
            //Already at the correct position.
            if (SyncDataMatchesTransform(_targetSyncData.GoalData, true))
                return;

            /* If a move rate is -1f then the data was snapped. There is no reason to move
             * towards. We'll still set the values because it's possible the object fell out of
             * sync, but we won't smooth it. */

            //Position.
            if (_moveRates.Position == -1f)
            {
                TargetTransform.SetPosition(_useLocalSpace, _targetSyncData.GoalData.Position);
            }
            else
            {
                TargetTransform.SetPosition(_useLocalSpace,
                    Vector3.MoveTowards(TargetTransform.GetPosition(_useLocalSpace), _targetSyncData.GoalData.Position, _moveRates.Position * Time.deltaTime)
                    );
            }
            //Rotation.
            if (_moveRates.Rotation == -1f)
            {
                TargetTransform.SetRotation(_useLocalSpace, _targetSyncData.GoalData.Rotation);
            }
            else
            {
                TargetTransform.SetRotation(_useLocalSpace,
                    Quaternion.RotateTowards(TargetTransform.GetRotation(_useLocalSpace), _targetSyncData.GoalData.Rotation, _moveRates.Rotation * Time.deltaTime)
                    );
            }
            //Scale.
            if (_moveRates.Scale == -1f)
            {
                TargetTransform.SetScale(_targetSyncData.GoalData.Scale);
            }
            else
            {
                TargetTransform.SetScale(
                    Vector3.MoveTowards(TargetTransform.GetScale(), _targetSyncData.GoalData.Scale, _moveRates.Scale * Time.deltaTime)
                    );
            }
        }


        /// <summary>
        /// Returns true if the passed in axes contains all axes.
        /// </summary>
        /// <param name="axes"></param>
        /// <returns></returns>
        private bool SnapAll(Axes axes)
        {
            return (axes == (Axes.X | Axes.Y | Axes.Z));
        }

        /// <summary>
        /// Returns true if the passed in SyncData values match this transforms values.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool SyncDataMatchesTransform(TransformSyncData data, bool precise)
        {
            if (data == null)
                return false;

            return (
                PositionMatches(data, null, precise) &&
                RotationMatches(data, null, precise) &&
                ScaleMatches(data, null, precise)
                );
        }

        /// <summary>
        /// Returns if this transform position matches data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool PositionMatches(TransformSyncData data, TargetSyncData targetData, bool precise)
        {
            if (data == null)
                return false;

            if (precise)
            {
                if (targetData == null)
                    return (TargetTransform.GetPosition(_useLocalSpace) == data.Position);
                else
                    return (targetData.GoalData.Position == data.Position);
            }
            else
            {
                float dist;
                if (targetData == null)
                    dist = Vector3.SqrMagnitude(TargetTransform.GetPosition(_useLocalSpace) - data.Position);
                else
                    dist = Vector3.SqrMagnitude(targetData.GoalData.Position - data.Position);
                return (dist < 0.0001f);
            }
        }
        /// <summary>
        /// Returns if this transform rotation matches data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool RotationMatches(TransformSyncData data, TargetSyncData targetData, bool precise)
        {
            if (data == null)
                return false;

            Quaternion rotation = (targetData == null) ? TargetTransform.GetRotation(_useLocalSpace) : targetData.GoalData.Rotation;
            if (precise)
                return rotation.Matches(data.Rotation);
            else
                return rotation.Matches(data.Rotation, 1f);
        }
        /// <summary>
        /// Returns if this transform scale matches data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool ScaleMatches(TransformSyncData data, TargetSyncData targetData, bool precise)
        {
            if (data == null)
                return false;

            Vector3 scale = (targetData == null) ? TargetTransform.GetScale() : targetData.GoalData.Scale;

            if (precise)
            {
                return (TargetTransform.GetScale() == data.Scale);
            }
            else
            {
                float dist = Vector3.SqrMagnitude(scale - data.Scale);
                return (dist < 0.0001f);
            }
        }


        /// <summary>
        /// Sends SyncData to the server. Only used with client auth.
        /// </summary>
        /// <param name="data"></param>
        [Command(channel = 0)]
        private void CmdSendSyncDataReliable(TransformSyncData data)
        {
            ClientDataReceived(data);
        }
        /// <summary>
        /// Sends SyncData to the server. Only used with client auth.
        /// </summary>
        /// <param name="data"></param>
        [Command(channel = 1)]
        private void CmdSendSyncDataUnreliable(TransformSyncData data)
        {
            ClientDataReceived(data);
        }

        /// <summary>
        /// Called on clients when server data is received.
        /// </summary>
        /// <param name="data"></param>
        [Server]
        private void ClientDataReceived(TransformSyncData data)
        {
            //Sent to self.
            if (base.hasAuthority)
                return;

            SyncProperties sp = (SyncProperties)data.SyncProperties;
            /* If not reliable then compare sequence id to ensure
             * this did not arrive out of order. */
            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Sequenced))
            {
                //If data Id is less than last Id then it's old data.
                if (data.SequenceId < _lastClientSequenceId)
                    return;

                _lastClientSequenceId = data.SequenceId;
            }

            ApplyGoalData(data, _targetSyncData);
            /* If server only then snap to target position. 
             * //EXTRAPOLATION may need to snap in intervals
             * if using server side extrapolation. */
            //if (base.isServerOnly)
            //{
            //    ApplyTransformSnapping(data, true);
            //}
            /* If not server only, so if client host, then set data
             * normally for smoothing. */
            //else
            //{
            /* By default use the preconfigured multiplier.
            * This is the multiplier to use under ideal conditions. */
            float interpolationMultiplier = ReturnInterpolationWithDeviance(_targetSyncData);
            _moveRates = new MoveRate();
            /* Position. */
            _moveRates.Position = (Vector3.Distance(TargetTransform.GetPosition(_useLocalSpace), data.Position) / ReturnSyncInterval()) * interpolationMultiplier;
            /* Rotation. */
            _moveRates.Rotation = (Quaternion.Angle(TargetTransform.GetRotation(_useLocalSpace), data.Rotation) / ReturnSyncInterval()) * interpolationMultiplier;
            /* Scale. */
            _moveRates.Scale = (Vector3.Distance(TargetTransform.GetScale(), data.Scale) / ReturnSyncInterval()) * interpolationMultiplier;

            ApplyTransformSnapping(data, false);
            _targetSyncData = new TargetSyncData(data, Time.time);
            //}
        }


        /// <summary>
        /// Snaps the transform to targetData where snapping is applicable.
        /// </summary>
        /// <param name="targetData">Data to snap from.</param>
        private void ApplyTransformSnapping(TransformSyncData targetData, bool snapAll)
        {
            SyncProperties sp = (SyncProperties)targetData.SyncProperties;

            if (snapAll || EnumContains.SyncPropertiesContains(sp, SyncProperties.Position))
            {
                //If to snap all.
                if (snapAll || SnapAll(_snapPosition))
                {
                    TargetTransform.SetPosition(_useLocalSpace, targetData.Position);
                }
                //Snap some or none.
                else
                {
                    //Snap X.
                    if (EnumContains.AxesContains(_snapPosition, Axes.X))
                        TargetTransform.SetPosition(_useLocalSpace, new Vector3(targetData.Position.x, TargetTransform.GetPosition(_useLocalSpace).y, TargetTransform.GetPosition(_useLocalSpace).z));
                    //Snap Y.
                    if (EnumContains.AxesContains(_snapPosition, Axes.Y))
                        TargetTransform.SetPosition(_useLocalSpace, new Vector3(TargetTransform.GetPosition(_useLocalSpace).x, targetData.Position.y, TargetTransform.GetPosition(_useLocalSpace).z));
                    //Snap Z.
                    if (EnumContains.AxesContains(_snapPosition, Axes.Z))
                        TargetTransform.SetPosition(_useLocalSpace, new Vector3(TargetTransform.GetPosition(_useLocalSpace).x, TargetTransform.GetPosition(_useLocalSpace).y, targetData.Position.z));
                }
            }

            /* Rotation. */
            if (snapAll || EnumContains.SyncPropertiesContains(sp, SyncProperties.Rotation))
            {
                //If to snap all.
                if (snapAll || SnapAll(_snapRotation))
                {
                    TargetTransform.SetRotation(_useLocalSpace, targetData.Rotation);
                }
                //Snap some or none.
                else
                {
                    /* Only perform snap checks if snapping at least one
                     * to avoid extra cost of calculations. */
                    if ((int)_snapRotation != 0)
                    {
                        /* Convert to eulers since that is what is shown
                         * in the inspector. */
                        Vector3 startEuler = TargetTransform.GetRotation(_useLocalSpace).eulerAngles;
                        Vector3 targetEuler = targetData.Rotation.eulerAngles;
                        //Snap X.
                        if (EnumContains.AxesContains(_snapRotation, Axes.X))
                            startEuler.x = targetEuler.x;
                        //Snap Y.
                        if (EnumContains.AxesContains(_snapRotation, Axes.Y))
                            startEuler.y = targetEuler.y;
                        //Snap Z.
                        if (EnumContains.AxesContains(_snapRotation, Axes.Z))
                            startEuler.z = targetEuler.z;

                        //Rebuild into quaternion.
                        TargetTransform.SetRotation(_useLocalSpace, Quaternion.Euler(startEuler));
                    }
                }
            }

            if (snapAll || EnumContains.SyncPropertiesContains(sp, SyncProperties.Scale))
            {
                //If to snap all.
                if (snapAll || SnapAll(_snapScale))
                {
                    TargetTransform.SetScale(targetData.Scale);
                }
                //Snap some or none.
                else
                {
                    //Snap X.
                    if (EnumContains.AxesContains(_snapScale, Axes.X))
                        TargetTransform.SetScale(new Vector3(targetData.Scale.x, TargetTransform.GetScale().y, TargetTransform.GetScale().z));
                    //Snap Y.
                    if (EnumContains.AxesContains(_snapScale, Axes.Y))
                        TargetTransform.SetPosition(_useLocalSpace, new Vector3(TargetTransform.GetScale().x, targetData.Scale.y, TargetTransform.GetScale().z));
                    //Snap Z.
                    if (EnumContains.AxesContains(_snapScale, Axes.Z))
                        TargetTransform.SetPosition(_useLocalSpace, new Vector3(TargetTransform.GetScale().x, TargetTransform.GetScale().y, targetData.Scale.z));
                }
            }
        }

        /// <summary>
        /// Sends SyncData to clients.
        /// </summary>
        /// <param name="data"></param>
        [ClientRpc(channel = 0)]
        private void RpcSendSyncDataReliable(TransformSyncData data)
        {
            ServerDataReceived(data);
        }
        /// <summary>
        /// Sends SyncData to clients.
        /// </summary>
        /// <param name="data"></param>
        [ClientRpc(channel = 1)]
        private void RpcSendSyncDataUnreliable(TransformSyncData data)
        {
            ServerDataReceived(data);
        }

        /// <summary>
        /// Called on clients when server data is received.
        /// </summary>
        /// <param name="data"></param>
        [Client]
        private void ServerDataReceived(TransformSyncData data)
        {
            //If client host exit method.
            if (base.isServer)
                return;

            //If owner of object.
            if (base.hasAuthority)
            {
                //Client authoritative, already in sync.
                if (_clientAuthoritative)
                    return;
                //Not client authoritative, but also not sync to owner.
                if (!_clientAuthoritative && !_synchronizeToOwner)
                    return;
            }

            SyncProperties sp = (SyncProperties)data.SyncProperties;
            /* If not reliable then compare sequence id to ensure
             * this did not arrive out of order. */
            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Sequenced))
            {
                //If data Id is less than last Id then it's old data.
                if (data.SequenceId < _lastServerSequenceId)
                    return;

                _lastServerSequenceId = data.SequenceId;
            }

            /* By default use the preconfigured multiplier.
             * This is the multiplier to use under ideal conditions. */
            float interpolationMultiplier = ReturnInterpolationWithDeviance(_targetSyncData);

            _moveRates = new MoveRate();
            /* Position. */
            _moveRates.Position = (Vector3.Distance(TargetTransform.GetPosition(_useLocalSpace), data.Position) / ReturnSyncInterval()) * interpolationMultiplier;
            /* Rotation. */
            _moveRates.Rotation = (Quaternion.Angle(TargetTransform.GetRotation(_useLocalSpace), data.Rotation) / ReturnSyncInterval()) * interpolationMultiplier;
            /* Scale. */
            _moveRates.Scale = (Vector3.Distance(TargetTransform.GetScale(), data.Scale) / ReturnSyncInterval()) * interpolationMultiplier;

            //Set goal data and make new target data.
            ApplyGoalData(data, _targetSyncData);
            ApplyTransformSnapping(data, false);
            _targetSyncData = new TargetSyncData(data, Time.time);
        }

        /// <summary>
        /// Returns an interpolation value to use based on data arrival deviance.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private float ReturnInterpolationWithDeviance(TargetSyncData data)
        {
            //If there is past data to calculate deviance from.
            if (data != null)
            {
                float timeDifference = Time.time - _targetSyncData.ReceivedTime;
                /* Get how much of a percent difference this data came in
                 * from the last. EG if data came in 30% faster or slower
                 * than expected then percentDifference would be 0.3f. */
                float percentDifference = Mathf.Abs(1f - (timeDifference / ReturnSyncInterval()));
                float limitedDifference = Mathf.Lerp(1f, 0.10f, percentDifference);
                _serverDataDevianceInterpolationMultiplier.Add(limitedDifference);
                return (_interpolationIntervalMultiplier * _serverDataDevianceInterpolationMultiplier.Value);
            }
            else
            {
                return _interpolationIntervalMultiplier;
            }
        }

        /// <summary>
        /// Modifies values within goalData based on what data was included in the packet.
        /// For example, if rotation was not included in the packet then the last datas rotation will be used, or transforms current rotation if there is no previous packet.
        /// </summary>
        private void ApplyGoalData(TransformSyncData goalData, TargetSyncData targetSyncData)
        {
            SyncProperties sp = (SyncProperties)goalData.SyncProperties;
            /* Begin by setting goal data using what has been serialized
             * via the writer. */
            //Position wasn't included.
            if (!EnumContains.SyncPropertiesContains(sp, SyncProperties.Position))
            {
                if (targetSyncData == null)
                    goalData.Position = TargetTransform.GetPosition(_useLocalSpace);
                else
                    goalData.Position = targetSyncData.GoalData.Position;
            }
            //Rotation wasn't included.
            if (!EnumContains.SyncPropertiesContains(sp, SyncProperties.Rotation))
            {
                if (targetSyncData == null)
                    goalData.Rotation = TargetTransform.GetRotation(_useLocalSpace);
                else
                    goalData.Rotation = targetSyncData.GoalData.Rotation;
            }
            //Scale wasn't included.
            if (!EnumContains.SyncPropertiesContains(sp, SyncProperties.Scale))
            {
                if (targetSyncData == null)
                    goalData.Scale = TargetTransform.GetScale();
                else
                    goalData.Scale = targetSyncData.GoalData.Scale;
            }
        }

        //EXTRAPOLATION
        ///// <summary>
        ///// Multiplier to apply for each extrapolation. This can be for distance or expected lerp time.
        ///// </summary>
        ///// <returns></returns>
        //private float ReturnExtrapolationMultiplier()
        //{
        //    if (_extrapolationDuration == ExtrapolationDurations.Off)
        //        return 1f;
        //    else if (_extrapolationDuration == ExtrapolationDurations.VeryShort)
        //        return 2f;
        //    else if (_extrapolationDuration == ExtrapolationDurations.Short)
        //        return 4f;
        //    else if (_extrapolationDuration == ExtrapolationDurations.Medium)
        //        return 8f;
        //    else if (_extrapolationDuration == ExtrapolationDurations.Long)
        //        return 16f;

        //    //Fall through.
        //    return 1f;
        //}

        private void OnValidate()
        {
            //Update interpolation percent just in case user changes values while testing.
            _interpolationIntervalMultiplier = ReturnInterpolationIntervalMultiplier();
        }

    }
}

