using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class GetCharacterEditor : EditorWindow
{
    private List<string> chars;
    private List<Entity> characterScripts;
    private CharacterData characterData;

    private int selectedIndex;

    [MenuItem("Tools/GetCharacterEditor")]
    public static void ShowGetCharacterEditor()
    {
        EditorWindow wnd = GetWindow<GetCharacterEditor>();
        wnd.titleContent = new GUIContent("Get Character Editor");
    }

    public void CreateGUI()
    {
        var allObjectGuids = AssetDatabase.FindAssets("t:Prefab");
        var allObjects = new List<GameObject>();

        chars = new List<string>();
        characterScripts = new List<Entity>();

        int count = 0;
        foreach (var guid in allObjectGuids)
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid)));

            if (allObjects[count].TryGetComponent<Character>(out Character character))
            {
                chars.Add(allObjects[count].name);
                characterScripts.Add(allObjects[count].GetComponent<Entity>());
                count++;
            }
        }

        characterData = FindFirstObjectByType<CharacterData>();
    }

    public void OnGUI()
    {
        selectedIndex = EditorGUILayout.Popup("Character : ", selectedIndex, chars.ToArray());
        EditorGUILayout.Space();

        if (GUILayout.Button("Add Character To Owned"))
        {
            AddCharacterToOwned();
        }

        if (GUILayout.Button("Remove Character To Owned"))
        {
            RemoveCharacterToOwned();
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("Get All Character"))
        {
            GetAllCharacters();
        }

        if (GUILayout.Button("Remove All Character"))
        {
            RemoveAllCharacters();
        }

        if (GUILayout.Button("Select All Character"))
        {
            SelectAllCharacters();
        }
    }

    private void AddCharacterToOwned()
    {
        characterData.AddOwnedCharacter(characterScripts[selectedIndex]);
    }

    private void RemoveCharacterToOwned()
    {
        characterData.RemoveOwnedCharacter(characterScripts[selectedIndex]);
    }

    private void GetAllCharacters()
    {
        foreach (var entity in characterScripts)
        {
            characterData.AddOwnedCharacter(entity);
        }
    }

    private void RemoveAllCharacters()
    {
        foreach (var entity in characterScripts)
        {
            characterData.RemoveOwnedCharacter(entity);
        }
    }

    private void SelectAllCharacters()
    {
        int count;
        foreach (var item in characterData.OwnedCharacterListData.CharListAsRankDictionary)
        {
            count = 0;
            foreach (var entity in item.Value.EntityAsCodeDict)
            {
                characterData.AddSelectedCharacter(entity.Value);

                count++;
                if(count == characterData.RankCountByRank[item.Key])
                {
                    break;
                }
            }
            
        }
    }
}
