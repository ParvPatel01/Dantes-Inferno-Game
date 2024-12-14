using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathUIScript : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowDeathScreen() {
        Debug.Log("Death Screen");
        gameObject.SetActive(true);
    }

    public void SetText(string text) {
        Debug.Log("Setting Text");
        transform.Find("DisplayTxt").GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}
