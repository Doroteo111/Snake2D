using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader // clase estatica --> no hace falta que este instanciada para usarla
                           // tiene todas sus variables y funciones tambien static
    {
        // Variable que guarda una función sin inputs ni output
        private static Action loaderCallbackAction;

        // Lista de nuestras escenas
        public enum Scene
        {
            Game,
            LoadingScene,
            MainMenu
        }

        private static Scene sceneAux;

        public static void Load(Scene scene)
        {
            // Asignas en loaderCallbackAction una función que no recibe parámetros y ejecuta la línea 25
            loaderCallbackAction = () =>
            {
                SceneManager.LoadScene(scene.ToString());
            };


            // Llamamos a la escena de carga
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
        }

        public static void LoaderCallback()
        {
            if (loaderCallbackAction != null)
            {
                loaderCallbackAction();
                loaderCallbackAction = null;
            }
        }


        // () => { cuerpo función }
        /*
         * private void NombreAux(){
         * cuerpo función
         * }
         */


    }

