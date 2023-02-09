using UnityEngine;
using System.Collections;


public class GoToMenu : MonoBehaviour
{
    IEnumerator Start()
    {
        // Wait for the loading scene manager to start
        yield return new WaitUntil(() => LoadingSceneManager.Instance != null);

        // Load the menu
        LoadingSceneManager.Instance.LoadScene(SceneName.Lobby, false);
    }
}
