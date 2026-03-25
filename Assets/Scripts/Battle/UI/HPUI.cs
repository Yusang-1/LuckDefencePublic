using UnityEngine;

public class HPUI : MonoBehaviour
{
    [SerializeField] RectTransform hpTransform;
    [SerializeField] Vector3 upVector;

    private Entity entity;

    private bool isMatched;
    public bool IsMatched => isMatched;

    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(entity.transform.position + upVector);
    }

    public void matchEntity(Entity entity)
    {
        this.entity = entity;

        entity.BattleData.HPChanged += OnSetUI;

        isMatched = true;
    }

    public void OnSetUI(int hp)
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        float value = (float)hp / entity.Data.MaxHp;
        hpTransform.localScale = new Vector3(value, 1, 1);

        if(hp <= 0)
        {
            ResetUI();

            gameObject.SetActive(false);
        }
    }

    public void ResetUI()
    {
        entity.BattleData.HPChanged -= OnSetUI;
        entity = null;
        isMatched = false;

        hpTransform.localScale = new Vector3(1, 1, 1);
    }
}
