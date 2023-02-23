using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.MainMenu
{
    public class PlayButton : MonoBehaviour
    {
        public void OnClickPlay()
        {
            SceneLoadingManager.LoadScene("InGame");
        }
    }
}


