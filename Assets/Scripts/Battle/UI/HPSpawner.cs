using System;
using UnityEngine;

public class HPSpawner : MonoBehaviour
{
    [SerializeField] private GameObject HPUIObject;
    [SerializeField] private Transform poolTransform;

    private HPUIController[] hpUIs;
    
    private const int minCreationAmount = 10;

    public void Initialize(StageSO stageData)
    {
        hpUIs = Array.Empty<HPUIController>();
        int maxEnemyCount = stageData.MaxEnemyCount;
        CreateHPUI(maxEnemyCount + minCreationAmount);
    }
    
    private void CreateHPUI(int amount)
    {
        int beforeLength = hpUIs.Length;
        int newSize = beforeLength + amount;
        HPUIController[] temp = new HPUIController[newSize];
        Array.Copy(hpUIs, temp, beforeLength);
        hpUIs = temp;
        
        GameObject hpUI;
        for (int i = beforeLength; i < newSize; i++)
        {
            hpUI = Instantiate(HPUIObject, poolTransform);
            hpUI.SetActive(false);

            hpUIs[i] = hpUI.GetComponent<HPUIController>();
        }
    }

    public HPUIController ActivateHP(Entity entity)
    {
        bool isSuccess = false;
        HPUIController hp = null;
        
        foreach(HPUIController hpUI in hpUIs)
        {
            if(hpUI.IsMatched == false)
            {
                hp = hpUI;
                hpUI.matchEntity(entity);
                isSuccess = true;
                break;
            }
        }
        
        if(isSuccess == false)
        {
            CreateHPUI(minCreationAmount);
            hp = hpUIs[hpUIs.Length - 1];
            hp.matchEntity(entity);
        }
        
        return hp;
    }

    public void OnDeactiveAllHP()
    {
        foreach(var hp in hpUIs)
        {
            if(hp.IsMatched)
            {
                hp.ResetUI();
                hp.gameObject.SetActive(false);
            }
        }
    }
}
