#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FirstGearGames.Mirrors.Assets.FlexNetworkTransforms.Editors
{

    [CustomEditor(typeof(FlexNetworkTransformChild))]
    public class FlexNetworkTransformChildEditor : FlexNetworkTransformBaseEditor
    {
        private SerializedProperty _target;

        protected override void OnEnable()
        {
            base.OnEnable();
            _target = serializedObject.FindProperty("Target");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //Transform.
            EditorGUILayout.LabelField("Transform");
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_target, new GUIContent("Target", "Transform to synchronize."));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();


        }

    }
}
#endif