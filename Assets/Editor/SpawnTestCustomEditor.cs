using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SpawnTestCustomEditor : EditorWindow
{
    private Platform selectedPlatform;
    private string myString;
    private int selectedIndex;
    private int spawnNum;
    private List<string> chars;
    private List<GameObject> characterObjects;
    private CharacterSpawner spawner;

    [MenuItem("Tools/SpawnTestEditor")]
    public static void ShowSpawnTestEditor()
    {
        EditorWindow wnd = GetWindow<SpawnTestCustomEditor>();
        wnd.titleContent = new GUIContent("Spawn Test Editor");
    }

    public void CreateGUI()
    {
        var allObjectGuids = AssetDatabase.FindAssets("t:Prefab");
        var allObjects = new List<GameObject>();

        chars = new List<string>();
        characterObjects = new List<GameObject>();

        int count = 0;
        foreach (var guid in allObjectGuids)
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid)));

            if (allObjects[count].TryGetComponent<Character>(out Character character))
            {
                chars.Add(allObjects[count].name);
                characterObjects.Add(allObjects[count]);
                count++;
            }
        }

        spawner = FindFirstObjectByType<CharacterSpawner>();
        
        spawnNum = 1;
    }

    public void OnGUI()
    {
        myString = EditorGUILayout.TextField("Platform : ", myString);
        selectedIndex = EditorGUILayout.Popup("Character : ", selectedIndex, chars.ToArray());

        spawnNum = EditorGUILayout.IntField("SpawnNum : ", spawnNum);

        if(GUILayout.Button("Button") && selectedPlatform != null)
        {
            SpawnTest();
        }
    }

    public void OnSelectionChange()
    {        
        GameObject selectedObject = Selection.activeGameObject;

        if(selectedObject.TryGetComponent<Platform>(out Platform platform))
        {
            selectedPlatform = platform;
            myString = selectedPlatform.name;
        }
        else
        {
            selectedPlatform = null;
            myString = "none";
        }
    }

    private void SpawnTest()
    {
        Entity entity = characterObjects[selectedIndex].GetComponent<Entity>();
        Vector3 position;
        SummonData data;

        for (int i = 0; i < spawnNum; i++)
        {
            position = selectedPlatform.GetPosition((entity.Data as CharacterSO).Rank);

            data = new SummonData((int)(entity.Data as CharacterSO).Rank, entity.Data.Code, selectedPlatform.Index, position);

            spawner.OrderToFactory(data);
        }
    }
}
