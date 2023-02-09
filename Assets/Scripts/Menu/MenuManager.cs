using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Menu[] menus;

    private void Start()
    {
        if(Instance == null) Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        bool isLieOn = false;
        foreach (var menu in menus)
        {
            if(menu.name == menuName)
            {
                menu.Open();
                isLieOn = menu.isLieOn;
            }
        }
        if(!isLieOn)
        {
            foreach (var menu in menus)
            {
                if(menu.name != menuName)
                {
                    menu.Close();
                }
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        bool isLieOn = menu.isLieOn;

        if (!isLieOn)
        {
            foreach(var menuItem in menus)
            {
                menuItem.Close();
            }
        }

        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        if(menu.isLieOn)
        {
            menu.Close();
        }
    }
    
}
