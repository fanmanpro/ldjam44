using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastSecondHackScript : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        RestartLevel();
    }
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
