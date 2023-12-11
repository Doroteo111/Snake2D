using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        timeBeforeLoading = Random.Range(5,10); // give a random number between 5-10 --> will be seconds
        
        StartCoroutine(PhrasesPerSeconds()); //Array text, show a message every 2 seconds randomly
        StartCoroutine(countDown()); // counter to help myself

    }
    private void Update()
    {
        ChangeScene();
 
    }

    private void ChangeScene()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > timeBeforeLoading) //when the random seconds comes to end, will change the scene
        {
            // Loader.LoaderCallback(); no va, he de fozar el scene manager
            SceneManager.LoadScene(2);
        }
    }
    private IEnumerator countDown() //preview every second (VISUAL) 
    {
        while (true)
        {
            counterText.text = $"{contadorEjemplo}";
            contadorEjemplo++;
            yield return new WaitForSeconds(1);
        
          
        }

    }

    public IEnumerator PhrasesPerSeconds()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, phrasesExamples.Length);
            tipsText.text = $">>{(phrasesExamples[randomIndex])}<<";
            yield return new WaitForSeconds(spawnRate);
           

        }
    }
}