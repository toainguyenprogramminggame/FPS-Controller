using Unity.Netcode;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public void OnClickHost()
    {
        NetworkManager.Singleton.StartHost();
        LoadingSceneManager.Instance.LoadScene(SceneName.Gameplay);
    }

    public void OnClickJoin()
    {
        NetworkManager.Singleton.StartClient();
    }

}
