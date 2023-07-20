using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LNK.MoreDeepFloor.CorpsSelectScene
{
    public class BackButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("InGame");
        }
    }
}


