using UnityEngine;
using TMPro;

public class MPUI : MonoBehaviour
{
    [SerializeField] RectTransform mpTransform;
    [SerializeField] TextMeshProUGUI mpText;

    private Entity entity;
    
    public void matchEntity(Entity entity)
    {
        this.entity = entity;
        OnSetUI(0);
        gameObject.SetActive(true);
        (entity.BattleData as ISkillUsableData).MPChanged += OnSetUI;
    }

    public void OnSetUI(int mp)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        float value = (float)mp / entity.Data.MaxMp;
        mpTransform.localScale = new Vector3(value, 1, 1);

        mpText.text = $"{mp}/{entity.Data.MaxMp}";
    }

    public void ResetUI()
    {
        if(entity == null) return;
        
        (entity.BattleData as ISkillUsableData).MPChanged -= OnSetUI;
        entity = null;
        gameObject.SetActive(false);
        mpTransform.localScale = new Vector3(1, 1, 1);
    }
}
