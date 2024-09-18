using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Changes the current scene to the scene with the given index
    public void ChangeScene(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }
}
