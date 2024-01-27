using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ReactiveAnimation))]
public class ReactiveAnimationEditor : Editor
{
    SerializedProperty animationStatesProp;

    ReactiveState selectedState = ReactiveState.NONE;

    private void OnEnable()
    {
        animationStatesProp = serializedObject.FindProperty("AnimationStates");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space(8);
        serializedObject.Update();

        ReactiveAnimation ra = (ReactiveAnimation)target;

        selectedState = (ReactiveState)EditorGUILayout.EnumPopup("State To Edit: ", selectedState);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(4));

        ReactiveAnimation.ReactiveAnimationState found = ra.AnimationStates.Find(x => x.ActiveState == selectedState);

        if (found != null)
        {
            int index = ra.AnimationStates.IndexOf(found);

            SerializedProperty state = animationStatesProp.GetArrayElementAtIndex(index);
            {
                EditorGUI.indentLevel++;

                SerializedProperty easeInTimeProp = state.FindPropertyRelative("EaseInTime");
                EditorGUILayout.PropertyField(easeInTimeProp);

                SerializedProperty relativeEaseProp = state.FindPropertyRelative("RelativeEase");
                EditorGUILayout.PropertyField(relativeEaseProp);

                SerializedProperty tintColorProp = state.FindPropertyRelative("TintColor");
                EditorGUILayout.PropertyField(tintColorProp);

                SerializedProperty overridesAffectedObjectsSettingsProp = state.FindPropertyRelative("OverrideAffectedObjectsSettings");
                EditorGUILayout.PropertyField(overridesAffectedObjectsSettingsProp);
                if(overridesAffectedObjectsSettingsProp.boolValue)
                {
                    EditorGUI.indentLevel++;

                    SerializedProperty affectedParentsProp = state.FindPropertyRelative("AffectedParents");
                    EditorGUILayout.PropertyField(affectedParentsProp, true);

                    SerializedProperty maxEffectDepthProp = state.FindPropertyRelative("MaxEffectDepth");
                    EditorGUILayout.PropertyField(maxEffectDepthProp);

                    SerializedProperty affectsTextProp = state.FindPropertyRelative("AffectsText");
                    EditorGUILayout.PropertyField(affectsTextProp);

                    EditorGUI.indentLevel--;
                }

                EditorGUI.indentLevel--;
            }

            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            if (GUILayout.Button("Remove State"))
            {
                ra.AnimationStates.RemoveAt(index);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("There's nothing here yet", MessageType.Info);

            if(GUILayout.Button("Add State"))
            {
                ReactiveAnimation.ReactiveAnimationState newState = new ReactiveAnimation.ReactiveAnimationState();
                newState.ActiveState = selectedState;

                ra.AnimationStates.Add(newState);
            }
        }

        serializedObject.ApplyModifiedProperties();

        if(GUI.changed)
        {
            EditorUtility.SetDirty(ra);
        }
    }
}
