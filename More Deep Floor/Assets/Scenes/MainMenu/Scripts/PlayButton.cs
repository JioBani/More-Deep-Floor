using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.MainMenu
{
    public class PlayButton : MonoBehaviour
    {
        public void OnClickPlay()
        {
            SceneManager.LoadScene("InGame");
        }
    }
}


