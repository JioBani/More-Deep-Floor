using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace LNK.MoreDeepFloor.Loading
{
    public class SceneLoadingManager : MonoBehaviour
    {
        [SerializeField] private Text loadingText;
        public static string nextScene;
        private WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        
        private void Start()
        {
            StartCoroutine(LoadScene());
        }

        public static void LoadScene(string sceneName)
        {
            nextScene = sceneName;
            SceneManager.LoadScene("Loading");
        }

        IEnumerator LoadScene()
        {
            if (nextScene == null) nextScene = "MainMenu";
            
            yield return null;
            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
            op.allowSceneActivation = false;
            while (!op.isDone)
            {
                if (op.progress < 0.9f)
                {
                    loadingText.text = ("로딩중 : " + (int)(op.progress * 100) + "%");
                }
                else
                {
                    loadingText.text = ("로딩중 : " + 90 + "%");
                    yield return waitForSeconds;
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}

