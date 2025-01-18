using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUi : MonoBehaviour
{
    public Image healthBar;
    public Image heartBar;

    public Player player;

    private void Start()
    {
        heartBar.fillAmount = player.currentHealth / 10;
    }

    private void Update()
    {
        healthBar.fillAmount = player.currentHealth / 10;
    }
}
