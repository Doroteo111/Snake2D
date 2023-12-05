using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using TMPro;

public class LoaderCallBack : MonoBehaviour
{
    #region Código Maria
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
    public TextMeshProUGUI counterText;

    //UI texts variables + array
    public TextMeshProUGUI tipsText;
    public string[] phrasesExamples;
    private float spawnRate = 2f;



    private void Start()
    {
        timeBeforeLoading=Random.Range( 5, 10);

      
        StartCoroutine(countDown()); // counter to help myself
        StartCoroutine(PhrasesPerSeconds()); //Array text, show a random message every 2 seconds randomly

    }
    private void Update()
    {
        ChangeScene();
 
    }

    private void ChangeScene()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > timeBeforeLoading) //cuando pasé el numero asignado cambiara de escena
        {
            SceneManager.LoadScene(0);
        }
    }
    private IEnumerator countDown() //previsualizar los segundos que pasan AJUDA VISUA PARA MI
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            counterText.text = $"{contadorEjemplo}";
            contadorEjemplo++;
        }

    }

    public IEnumerator PhrasesPerSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
           int randomIndex = Random.Range(0, phrasesExamples.Length);
           tipsText.text = $">>{(phrasesExamples[randomIndex])}<<";

        }
    }



    // CTRL + K + C --> Comentar selección
    // CTRL + K + U --> Descomentar selección
}