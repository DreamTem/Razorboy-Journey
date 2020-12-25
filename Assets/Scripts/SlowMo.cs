using UnityEngine;
using System.Collections;

public class SlowMo : MonoBehaviour
{ 
    public float slow, maxSlow = 15;
    float currentAmount = 0f;
    float maxAmount = 5f;
    bool CanTmStop=true;
    float slowed;
    public static bool slowEnabled;
    public void Start() {
        slowEnabled = false;
        slow=15;
    }
    void FixedUpdate()
    { 
        //stamina -,when time was slowed
        if (slowEnabled)
        {
            StartCoroutine(SlowStamina());
        }

        //time slow escaping
        if(Time.timeScale==0.3f){
            Invoke("Can",1.5f);        
        }

        //can time stop bool switch
        if (slow >= maxSlow)
        {
            CanTmStop = true;
        }

        //time slow recover
        if(Time.timeScale==1&& !CanTmStop && slow<maxSlow)
        {
            slow += Time.deltaTime;   
        }
        slowed=Input.GetAxis("Slowing");
        //time slowing
        if (slowed != 0 && !CheatCode.cheatStop && CanTmStop && PlayerAttack.stamina >= 3 && !MoveFox.dialogStop)
        {
            Time.timeScale=.3f; 
            slow-=15;
            slowEnabled = true;
            CanTmStop =false;
        }


        if (Time.timeScale == 0.03f)
        {

            currentAmount += Time.deltaTime;
        }

        if (currentAmount > maxAmount)
        {

            currentAmount = 0f;
            Time.timeScale = 1.0f;

        }

    }
    //stamina -,when time has slowed coroutine
    IEnumerator SlowStamina()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerAttack.stamina -= 0.046f;
    }

    //time slow escaping
    void Can(){
        slowEnabled = false;
        Time.timeScale=1;
    }
}