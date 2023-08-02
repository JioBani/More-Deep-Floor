using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.Common
{
    public class SceneController : MonoBehaviour
    {
        /*
        public static SceneController instance;
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
          
            DontDestroyOnLoad(gameObject);
        }
        */

        public void MoveToInGame()
        {
            SceneManager.LoadScene("InGame");
        }

        public void MoveToMain()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void MoveToCorpsSelect()
        {
            SceneManager.LoadScene("CorpsSelect");
        }
    }
}


