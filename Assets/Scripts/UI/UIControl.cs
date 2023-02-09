using UnityEngine;

public class UIControl : MonoBehaviour
{
    public Transform charactersHolder;

    private void Awake()
    {
    
    }

    int indexSelectedCharacter = 0;
    public void SelectCharacter(int add)
    {
        indexSelectedCharacter += add;
        if(indexSelectedCharacter < 0) indexSelectedCharacter = charactersHolder.childCount - 1;

        indexSelectedCharacter = indexSelectedCharacter % charactersHolder.childCount;
        int i = 0;
        foreach(Transform element in charactersHolder)
        {
            if(indexSelectedCharacter == i)
                element.gameObject.SetActive(true);
            else element.gameObject.SetActive(false);
            i++;
        }
    }




    public enum ListCharacter
    {
        Ninja = 1,
        AcMa = 2,
        ChienBinh = 3,
        ConDi = 4,
        Queen = 5,
        Rabbit = 6,
        Soldier = 7
    }
}
