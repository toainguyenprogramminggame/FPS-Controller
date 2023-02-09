using UnityEngine;

public class Menu : MonoBehaviour
{
    [HideInInspector] public string menuName;
    public bool isOpening;
    public bool isLieOn;

    private void Start()
    {
        GetName();
    }
    void GetName()
    {
        menuName = gameObject.name;
    }
    public void Open()
    {
        isOpening = true;
        gameObject.SetActive(isOpening);
    }
    public void Close()
    {
        isOpening = false;
        gameObject.SetActive(isOpening);
    }

}
