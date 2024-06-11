#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(SpawnData))]
public class SpawnDataEditor : Editor
{
    private ReorderableList reorderableList;

    private void OnEnable()
    {
        reorderableList = new ReorderableList(serializedObject,
            serializedObject.FindProperty("parentsWithChildren"),
            true, true, true, true);

        reorderableList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Parents with Children");
        };

        reorderableList.elementHeightCallback = (index) => {
            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            var children = element.FindPropertyRelative("children");
            return EditorGUI.GetPropertyHeight(children, true) + EditorGUIUtility.singleLineHeight * 5 + 10; // Extra space for the thicker separator and title
        };

        reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            var name = element.FindPropertyRelative("name");
            var parent = element.FindPropertyRelative("parent");
            var children = element.FindPropertyRelative("children");

            rect.y += 2;

            // Draw title (name) above the fields
            var titleRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(titleRect, name.stringValue, EditorStyles.boldLabel);

            rect.y += EditorGUIUtility.singleLineHeight + 2;
            var nameRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(nameRect, name, new GUIContent("Name"));

            rect.y += EditorGUIUtility.singleLineHeight + 2;
            var parentRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(parentRect, parent, new GUIContent("Parent"));

            rect.y += EditorGUIUtility.singleLineHeight + 2;
            var childrenRect = new Rect(rect.x, rect.y, rect.width, EditorGUI.GetPropertyHeight(children, true));
            EditorGUI.PropertyField(childrenRect, children, new GUIContent("Children"), true);

            // Draw thick separator line
            rect.y += EditorGUI.GetPropertyHeight(children, true) + 5;
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, 2), Color.red); // Change the color and thickness here
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}

#endif