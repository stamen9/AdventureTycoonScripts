using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
    string AreaText = "";
    static string keyString;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ItemDatabase m_Database = (ItemDatabase)target;
        if(GUILayout.Button("Add Item"))
        {
            m_Database.AddItemFromUI();
        }

        AreaText = EditorGUILayout.TextField("Item key to remove:", AreaText);

        if (GUILayout.Button("Remove Item"))
        {
            m_Database.RemoveItemFromUI(AreaText);
        }
    }
    void OnInspectorUpdate()
    {
        Repaint();
    }
}
