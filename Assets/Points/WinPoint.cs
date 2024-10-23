using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel; 
    [SerializeField] private GameObject InstructionPanel; 
    private void OnCollisionEnter(Collision collision)
    {
        WinPanel.SetActive(true);
        InstructionPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
