#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FirstGearGames.Mirrors.Assets.FlexNetworkTransforms.Editors
{

    [CustomEditor(typeof(FlexNetworkTransformBase), true)]
    public class FlexNetworkTransformBaseEditor : Editor
    {
        private SerializedProperty _useLocalSpace;

        private SerializedProperty _intervalType;
        private SerializedProperty _synchronizeInterval;

        private SerializedProperty _reliable;
        private SerializedProperty _preciseSynchronization;
        //private SerializedProperty _extrapolationDuration;
        private SerializedProperty _automaticInterpolation;
        private SerializedProperty _interpolationStrength;

        private SerializedProperty _clientAuthoritative;
        private SerializedProperty _synchronizeToOwner;


        private SerializedProperty _synchronizePosition;
        private SerializedProperty _snapPosition;

        private SerializedProperty _synchronizeRotation;
        private SerializedProperty _snapRotation;

        private SerializedProperty _synchronizeScale;
        private SerializedProperty _snapScale;

        protected virtual void OnEnable()
        {
            _useLocalSpace = serializedObject.FindProperty("_useLocalSpace");

            _intervalType = serializedObject.FindProperty("_intervalType");
            _synchronizeInterval = serializedObject.FindProperty("_synchronizeInterval");

            _reliable = serializedObject.FindProperty("_reliable");
            _preciseSynchronization = serializedObject.FindProperty("_preciseSynchronization");
            //_extrapolationDuration = serializedObject.FindProperty("_extrapolationDuration");
            _automaticInterpolation = serializedObject.FindProperty("_automaticInterpolation");
            _interpolationStrength = serializedObject.FindProperty("_interpolationStrength");

            _clientAuthoritative = serializedObject.FindProperty("_clientAuthoritative");
            _synchronizeToOwner = serializedObject.FindProperty("_synchronizeToOwner");

            _synchronizePosition = serializedObject.FindProperty("_synchronizePosition");
            _snapPosition = serializedObject.FindProperty("_snapPosition");

            _synchronizeRotation = serializedObject.FindProperty("_synchronizeRotation");
            _snapRotation = serializedObject.FindProperty("_snapRotation");

            _synchronizeScale = serializedObject.FindProperty("_synchronizeScale");
            _snapScale = serializedObject.FindProperty("_snapScale");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            FlexNetworkTransformBase data = (FlexNetworkTransformBase)target;

            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            //Transform.
            EditorGUILayout.LabelField("Space");
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_useLocalSpace, new GUIContent("Use LocalSpace", "True to synchronize using localSpace rather than worldSpace. If you are to child this object throughout it's lifespan using worldspace is recommended. However, when using worldspace synchronization may not behave properly on VR. LocalSpace is the default."));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            //Timing.
            EditorGUILayout.LabelField("Timing");
            EditorGUI.indentLevel++;

            //If not reliable and interval type is set to interval.
            if (!_reliable.boolValue && _intervalType.intValue == 0)
                EditorGUILayout.HelpBox("For best results use FixedUpdate Interval Type when not using Reliable messages.", MessageType.Warning);
            EditorGUILayout.PropertyField(_intervalType, new GUIContent("Interval Type", "How to operate synchronization timings. Timed will synchronized every specified interval while FixedUpdate will synchronize every FixedUpdate."));
            if (_intervalType.intValue == 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_synchronizeInterval, new GUIContent("Synchronize Interval", "How often to synchronize this transform."));
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();



            //Synchronization Processing.
            EditorGUILayout.LabelField("Synchronization Processing");
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(_reliable, new GUIContent("Reliable", "True to synchronize using the reliable channel. False to synchronize using the unreliable channel. Your project must use 0 as reliable, and 1 as unreliable for this to function properly. This feature is not supported on TCP transports."));
            EditorGUILayout.PropertyField(_preciseSynchronization, new GUIContent("Precise Synchronization", "True to synchronize data anytime it has changed. False to allow greater differences before synchronizing."));


            EditorGUILayout.HelpBox("Extrapolation has temporarily been disabled while it is receiving a rework. Sorry for the inconvienence.", MessageType.Warning);
            //bool fastInterval = (_intervalType.intValue == 1 || (_intervalType.intValue == 0 && _synchronizeInterval.floatValue < 0.1f));
            ////If reliable, short sync interval, and using VeryShort or Short extrapolation.
            //if (_reliable.boolValue && fastInterval && _extrapolationDuration.intValue > 0 && _extrapolationDuration.intValue < 3)
            //    EditorGUILayout.HelpBox("Extrapolation will be more likely to jitter on bad connections when using short intervals with reliable transports, and low extrapolation values. If you find this to be true either increase your synchronization intervals, or increase extrapolation duration.", MessageType.Warning);
            ////Not reliable, fast interval, longer extrapolation.
            //else if (!_reliable.boolValue && fastInterval && _extrapolationDuration.intValue > 1)
            //    EditorGUILayout.HelpBox("Extrapolation can generally be set to Very Short with great results while using unreliable transports and FixedUpdate.", MessageType.Info);
            //EditorGUILayout.PropertyField(_extrapolationDuration, new GUIContent("Extrapolation Duration", "How long to extrapolate transform for. Useful to improve fluid movement for players with bad connections. May want to use Short or Off for twitch response games, such as shooters."));

            EditorGUILayout.PropertyField(_automaticInterpolation, new GUIContent("Automatic Interpolation", "True to automatically determine interpolation strength. False to specify your own value. Interpolation is used to reduce stutter for unreliable connections."));
            if (_automaticInterpolation.boolValue == false)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_interpolationStrength, new GUIContent("Interpolation Strength", "How strongly to interpolate to server results. Higher values will result in more real-time results but may result in occasional stutter during network instability."));
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();



            //Authority.
            EditorGUILayout.LabelField("Authority");
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(_clientAuthoritative, new GUIContent("Client Authoritative", "True if using client authoritative movement."));
            if (_clientAuthoritative.boolValue == false)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_synchronizeToOwner, new GUIContent("Synchronize To Owner", "True to synchronize server results back to owner. Typically used when you are sending inputs to the server and are relying on the server response to move the transform."));
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();



            //Synchronize Properties.
            EditorGUILayout.LabelField("Synchronized Properties");
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(_synchronizePosition, new GUIContent("Position", "Synchronize options for position."));
            if (_synchronizePosition.intValue == 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_snapPosition, new GUIContent("Snap Position", "Euler axes on the position to snap into place rather than move towards over time."));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_synchronizeRotation, new GUIContent("Rotation", "Synchronize options for position."));
            if (_synchronizeRotation.intValue == 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_snapRotation, new GUIContent("Snap Rotation", "Euler axes on the rotation to snap into place rather than move towards over time."));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_synchronizeScale, new GUIContent("Scale", "Synchronize options for scale."));
            if (_synchronizeScale.intValue == 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_snapScale, new GUIContent("Snap Scale", "Euler axes on the scale to snap into place rather than move towards over time."));
                EditorGUI.indentLevel--;
            }

            if (EditorGUI.EndChangeCheck())
            {
                data.SetSnapPosition((Axes)_snapPosition.intValue);
                data.SetSnapRotation((Axes)_snapRotation.intValue);
                data.SetSnapScale((Axes)_snapScale.intValue);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}
#endif