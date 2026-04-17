using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "Scene Entry Point Data", menuName = "Scriptable Objects/Scene Entry Point Data", order = 1)]
public class SceneEntryPointData : ScriptableObject
{
    [SerializeField] private SceneNameManagerPair[] nameManagerPair;
    
    public Dictionary<string, Manager> SceneManagers;
    
    private IManagerSceneEntry currentSceneManagerSceneEntry;
    
    [Serializable]
    private struct SceneNameManagerPair
    {
        public string SceneName;
        public Manager SceneManager;
    }
    
    public IEnumerator SceneChanged(string sceneName)
    {
        if(SceneManagers == null)
        {
            SceneManagers = new Dictionary<string, Manager>();
            for(int i = 0; i < nameManagerPair.Length; i++)
            {
                SceneManagers.Add(nameManagerPair[i].SceneName, nameManagerPair[i].SceneManager);
            }
        }
        
        if(currentSceneManagerSceneEntry != null)
        {
            currentSceneManagerSceneEntry.DestroyPrevUIAfterLoad();
        }
        
        if(SceneManagers.ContainsKey(sceneName))
        {
            Manager manager = Instantiate(SceneManagers[sceneName]);
            IManagerSceneEntry managerSceneEntry = manager as IManagerSceneEntry;
            yield return manager.StartCoroutine(managerSceneEntry.Initialize());
            currentSceneManagerSceneEntry = managerSceneEntry;
        }
        else
        {
            Debug.LogError($"Scene {sceneName} does not have a manager assigned in SceneEntryPointData.");
        }
    }
}
