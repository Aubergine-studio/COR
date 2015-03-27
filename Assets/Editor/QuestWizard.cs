using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class QuestWizard : EditorWindow
{
    private string questName = "New quest";

    List<GameObject> tasks = new List<GameObject>();
    List<TaskType> type = new List<TaskType>();
    List<int> countInTask = new List<int>();

    [MenuItem("Window/QuestWizard")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(QuestWizard));
    }

    void OnGUI()
    {
        questName = EditorGUILayout.TextField("Quest name:", questName);

        for (int i = 0; i < tasks.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            type[i] = (TaskType)EditorGUILayout.EnumPopup("Type", type[i]);
            switch (type[i])
            {
                case TaskType.none:
                    EditorGUILayout.LabelField("Set task type.");
                    break;
                case TaskType.collect:
                case TaskType.kill:
                    tasks[i] = EditorGUILayout.ObjectField("Target Oject", tasks[i], typeof(GameObject), true) as GameObject;
                    countInTask[i] = EditorGUILayout.IntField("Count", countInTask[i]);
                    break;
            }
            if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
            {
                DelateTask(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            AddTask();
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create"))
            CreateQuest();
    }

    void CreateQuest()
    {
        GameObject newQuest = new GameObject(questName);
        newQuest.tag = "Quest";

        newQuest.AddComponent<Quest>();

        for (int i = 0; i < tasks.Count; i++)
        {
            switch (type[i])
            {
                case TaskType.collect:
                    Task newTask = newQuest.AddComponent<Collect>();
                    newTask.target = tasks[i];
                    newTask.count = countInTask[i];
                    break;
            }
        }

        PrefabUtility.CreatePrefab("Assets/Resources/Quests/" + newQuest.name + ".prefab", newQuest);
        DestroyImmediate(newQuest);
    }

    void AddTask()
    {
        tasks.Add(null);
        type.Add(new TaskType());
        countInTask.Add(1);
    }

    void DelateTask(int index)
    {
        tasks.RemoveAt(index);
        type.RemoveAt(index);
        countInTask.RemoveAt(index);
    }
}
