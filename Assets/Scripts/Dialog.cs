using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDispl;
    [SerializeField]
    private GameObject text,contin;
    [SerializeField]
    private string[] sentences;
    private int index;
    public static float continues,maxContinues;
    public bool canContinue;

    public float speed;
    private void Start() 
    {
        canContinue = false;
        contin.SetActive(false);
        MoveFox.dialogStop = true;
        continues = 0;
        maxContinues = this.sentences.Length;
        //typing start
        StartCoroutine(Type());
    }
    private void Update()
    {
        if(textDispl.text == sentences[index])
        {
            contin.SetActive(true);
            canContinue = true;
        }
        //continue button close
        if (continues >= maxContinues)
        {
            Invoke("Close", 0.1f);
        }
        //next sentense by pressing
        if(MoveFox.dialogStop && Input.GetKeyDown(KeyCode.JoystickButton9) && canContinue)
        {
            NextSent();
        }
    }
    //showing next letter
    private IEnumerator Type(){
       foreach(char letter in sentences[index].ToCharArray())
       {
           textDispl.text += letter;
           yield return new WaitForSeconds(speed);
       }
   }
    //Next sentence display
   private void NextSent(){
       contin.SetActive(false);
        continues++;
        if (index < sentences.Length - 1){
           index++;
           textDispl.text = "";   
           StartCoroutine(Type());   
           canContinue = false;
       }
           else
           {
               textDispl.text = "";
           }
       }
    //closing
    private void Close()
    {
        MoveFox.dialogStop = false;
        contin.gameObject.SetActive(false);
        text.SetActive(false);
        gameObject.SetActive(false);
        index = 0;
        continues = 1;
    }
   }
