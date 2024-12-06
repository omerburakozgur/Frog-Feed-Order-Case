using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject buttonParent;

    [SerializeField] private Button vibrationButton;
    [SerializeField] private Button soundButton;

    [SerializeField] private TextMeshProUGUI moveCountText;
    [SerializeField] private TextMeshProUGUI levelText;


    private void Start()
    {
        GameManager.Instance.OnMoveCountChange += GameManager_OnMoveCountChange; ;  

        settingsButton.onClick.AddListener(ToggleSettingsPanel);

        vibrationButton.onClick.AddListener(() =>
        {


        });

        soundButton.onClick.AddListener(() =>
        {


        });
    }

    private void GameManager_OnMoveCountChange(object sender, System.EventArgs e) {
        moveCountText.text = GameManager.Instance.GetMoveCount().ToString() + " MOVES";
    }

    private void ToggleSettingsPanel()
    {
        // toggle vibration and sound buttons
        if (buttonParent.activeSelf)
        {
            buttonParent.SetActive(false);
        }
        else
        {
            buttonParent.SetActive(true);

        }

    }
}
