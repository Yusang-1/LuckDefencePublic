using UnityEngine;
using System;
using TMPro;

public class HPUI : MonoBehaviour
{
    public event Action HPZero;
    
    [SerializeField] RectTransform hpTransform;
    [SerializeField] TextMeshProUGUI hpText;

    private Entity entity;
    
    private bool isMatched;
    public bool IsMatched => isMatched;
    
    public void matchEntity(Entity entity)
    {
        this.entity = entity;
        OnSetUI(entity.Data.MaxHp);
        gameObject.SetActive(true);
        isMatched = true;
    }

    public void OnSetUI(int hp)
    {
        if(entity == null) return;
        
        float value = (float)hp / entity.Data.MaxHp;
        hpTransform.localScale = new Vector3(value, 1, 1);
        
        hpText.text = $"{hp}/{entity.Data.MaxHp}";

        if(hp <= 0)
        {
            HPZero?.Invoke();
        }
    }

    public void ResetUI()
    {
        entity = null;
        gameObject.SetActive(false);
        hpTransform.localScale = new Vector3(1, 1, 1);
        isMatched = false;
    }
}
