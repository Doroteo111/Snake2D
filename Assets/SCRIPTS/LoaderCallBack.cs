using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using TMPro;

public class LoaderCallBack : MonoBehaviour
{
    #region Codigo Maria
    // que maria escrba luego escribo

    //en el`priemr frame es true, al segundo frame sera false
    /*private bool firstUpdate = true;

    private void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            Loader.LoaderCallback();
        }
    }*/
    #endregion
    //Time Loading variables
    private float timeBeforeLoading;
    private float timeElapsed;

    //corrutine variables
    private float contadorEjemplo = 1f;
    public TextMeshProUGUI text;

    //UI texts variables
    public TextMeshProUGUI tipsText;
    public string[] phrasesExamples;
   

    private void Start()
    {
        timeBeforeLoading=Random.Range( 5, 10);

      
        StartCoroutine(countDown());
       // PhrasesPerSeconds();
       


    }
    private void Update()
    {
        timeElapsed += Time.deltaTime;
      
        if (timeElapsed > timeBeforeLoading) //cuando pasé el numero asignado cambiara de escena
        {
            SceneManager.LoadScene(0);
        }
        //codigo array
    }
    private IEnumerator countDown() //previsualizar los segundos que pasan AJUDA VISUA PARA MI
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            text.text = $"{contadorEjemplo}";
            contadorEjemplo++;
        }

    }

    // private IEnumerator PhrasesPerSeconds()
    // {
    //     while (true) 
    //     {
    //         yield return new WaitForSeconds(3);
    //         tipsText.text= $"{namesExamples}";
    //     }
    // }

    //public void ExamplesList()
    // {
    //     namesExamples.Add("una frase random para ver si pinta");
    // }

    // CTRL + K + C --> Comentar selección
    // CTRL + K + U --> Descomentar selección
}