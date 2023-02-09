using UnityEngine;

public class Health : MonoBehaviour
{
    float health = 200;

    public void ChangeHealth(float amount)
    {
        health += amount;
    }
}
