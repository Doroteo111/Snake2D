using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderCallBack : MonoBehaviour
{
    // que maria escrba luego escribo

    //en el`priemr frame es true, al segundo frame sera false
    private bool firstUpdate = true;

    private void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            Loader.LoaderCallback();
        }
    }

}
