using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{
    public void Solo()
    {
        PlayerPrefs.SetInt("Multiplayer", 0);
    }
}
