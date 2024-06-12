using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject healthBar;

    private RectTransform healthTrans;

    private void Start()
    {
        healthTrans = healthBar.GetComponent<RectTransform>();
    }

    public void UpdateBar(float maxHealth, float health)
    {
        Debug.Log("Wow, doing stuff rn");
        float barWith = healthTrans.sizeDelta.x / maxHealth * health;
        healthTrans.sizeDelta = new Vector2(barWith, healthTrans.sizeDelta.y);
    }
}
