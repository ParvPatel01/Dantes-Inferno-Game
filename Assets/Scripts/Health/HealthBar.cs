using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider totalhealthBar;

    private void Start()
    {
        totalhealthBar.value = playerHealth.currentHealth / 100;
    }
    private void Update()
    {
        totalhealthBar.value = playerHealth.currentHealth / 100;
    }
}
