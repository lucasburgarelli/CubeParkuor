using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject InstructionPanel;
    [SerializeField] private Transform TransformPlayer;

    public void OnRestartButton()
    {
        WinPanel.SetActive(false);
        InstructionPanel.SetActive(true);
        TransformPlayer.position = new Vector3(0, 5, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
