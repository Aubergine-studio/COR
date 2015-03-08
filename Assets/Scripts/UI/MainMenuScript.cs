using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public void NewGame()
    {
        Application.LoadLevel(1);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
