using UnityEngine;
using System.Collections;
using UnityEditor;

public class SpriteChanger : EditorWindow
{
    private Sprite newSprite;

    [MenuItem("Window/SpriteChanger")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SpriteChanger));
    }

    void OnGUI()
    {
        GameObject[] tabGameObject = Selection.gameObjects;
        newSprite = EditorGUILayout.ObjectField("New sprite: ", newSprite, typeof(Sprite), true) as Sprite;
        if (GUILayout.Button("Change!") && newSprite != null)
            foreach (GameObject obj in tabGameObject)
            {
                obj.GetComponent<SpriteRenderer>().sprite = newSprite;
            }
    }
}
