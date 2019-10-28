using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{

    public Button continue_;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.Level == 2)
        {
            continue_.gameObject.active = false;
        } else
        {
            continue_.gameObject.active = true;
        }
    }
}
