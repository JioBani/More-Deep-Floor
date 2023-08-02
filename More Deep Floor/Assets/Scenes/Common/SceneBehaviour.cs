using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.Common
{
    public class SceneBehaviour : MonoBehaviour
    {
        // called first
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            BehaviourOnEnable();
        }

        protected virtual void BehaviourOnEnable()
        {
        
        }

        // called second
        protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
        
        }
    
        // called when the game is terminated
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

}

