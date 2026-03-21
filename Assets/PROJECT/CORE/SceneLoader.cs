using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] Scenes;

    private void LoadAllScenes()
    {
        foreach (string scene in Scenes)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
    
    void Start()
    {
        LoadAllScenes();
    }
}
