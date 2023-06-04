using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    private void Awake() {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textMesh.text = System.TimeSpan.FromSeconds(WaveTimer.s_Instance.timer).ToString(@"mm\:ss");
    }    
}
