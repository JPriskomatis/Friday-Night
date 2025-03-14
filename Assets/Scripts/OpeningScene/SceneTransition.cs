using GlobalSpace;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OpeningScene
{
    public class SceneTransition : MonoBehaviour
    {
        bool canTransition;

        void Awake()
        {
            //Load next scene;
            StartCoroutine(LoadScene());
        }


        public void StartGame()
        {
            StartCoroutine(TransitionToScene());
        }
        IEnumerator LoadScene()
        {

            yield return null;

            AsyncOperation asyncOpe = SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
            asyncOpe.allowSceneActivation = false;

            while (!asyncOpe.isDone)
            {
                if (asyncOpe.progress >= 0.9f)
                {
                    canTransition = true;
                    break;
                }
                yield return null;
            }
        }

        IEnumerator TransitionToScene()
        {
            if (canTransition)
            {
                yield return new WaitForSeconds(3f);
                SceneManager.LoadScene(GlobalConstants.GAME_SCENE);
            }
        }


    }

}