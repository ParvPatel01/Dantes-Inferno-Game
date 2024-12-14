using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBarScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Slider dashBar;

    private void Start()
    {
        dashBar.value = playerMovement.dashBarValue;
    }
    private void Update()
    {
        dashBar.value = playerMovement.dashBarValue;
    }
   
}
