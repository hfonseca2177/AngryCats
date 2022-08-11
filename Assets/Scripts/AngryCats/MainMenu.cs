using UnityEngine;
using UnityEngine.SceneManagement;

namespace AngryCats
{
    //Menu UI Script
    public class MainMenu : MonoBehaviour
    {

        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
