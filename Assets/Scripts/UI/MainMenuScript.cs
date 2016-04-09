using UnityEngine;

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
