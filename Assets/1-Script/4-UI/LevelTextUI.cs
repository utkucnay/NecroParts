using UnityEngine;
using TMPro;

public class LevelTextUI : MonoBehaviour 
{
    public TextMeshProUGUI levelText; 
    private void Awake() {
        levelText = GetComponent<TextMeshProUGUI>();    
    }
    private void Update() {
        levelText.text = "Lv " + SoulManager.s_Instance.level;       
    }    
}