using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject healthBar;
    private RectTransform healthTrans;

    private void Start()
    {
        if (healthBar != null)
        {
            healthTrans = healthBar.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("Health bar GameObject is not assigned.");
        }
    }

    public void UpdateBar(float maxHealth, float health)
    {
        if (healthTrans != null)
        {
            float barWidth = healthTrans.sizeDelta.x / maxHealth * health;
            healthTrans.sizeDelta = new Vector2(barWidth, healthTrans.sizeDelta.y);
        }
        else
        {
            Debug.LogError("RectTransform component is not assigned.");
        }
    }
}
