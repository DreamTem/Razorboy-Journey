using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialoger : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueManager, contin,dialogueText;
    private bool dialoged = false;
    private void Start()
    {
        dialogueText.SetActive(false);
        contin.SetActive(false);
        dialogueManager.SetActive(false);
    }
    private void Update() 
    {
        //dialog activate on gamepad
        if (MoveFox.dialogEnter && Input.GetKeyDown(KeyCode.JoystickButton9) && Dialog.continues < 13 && !dialoged)
        {
            MoveFox.dialogStop = true;
            dialogueManager.gameObject.SetActive(true);
            contin.gameObject.SetActive(true);
            dialogueText.SetActive(true);
            dialoged = true;
        }
    }
}
