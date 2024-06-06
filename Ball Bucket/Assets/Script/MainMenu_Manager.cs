using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        else
        {
            PlayerPrefs.SetInt("Star", 0);
            PlayerPrefs.SetInt("Level", 1);
            SceneManager.LoadScene(1);
        }
    }





}
