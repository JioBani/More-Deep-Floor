using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshProOutline : MonoBehaviour
{
    public float width;
    void Awake()
    {
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.outlineWidth = width;
        textmeshPro.outlineColor = new Color32(255, 128, 0, 255);
    }
}
