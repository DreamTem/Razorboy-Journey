using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSummon : MonoBehaviour
{
    public bool drawDebugRaycasts = true;
    public GameObject rangeControlCar,mainCamera,SpPos;
    public static bool carActive,canSummon;
    public float summons=0;
    public Animator animator;
    public LayerMask objectLayer;
    private RaycastHit2D rayc,rayc2;
    // Start is called before the first frame update
    void Start()
    {
        carActive = false;
        canSummon = false;
    }
    private void Update() 
    {
        //wall bloching raycast
        rayc = Raycast(new Vector2(0,0),Vector2.right,1.65f);
        if((rayc && summons < 4) || summons >= 4)
        {
            canSummon=false;
        }
        else if(summons < 5 && !carActive)
        {
            canSummon=true;
        }
        if(carActive)
        {
            MoveFox.rn = false;
            MoveFox.running = false;
            animator.SetTrigger("OR");
            mainCamera.gameObject.SetActive(false);
        }
        else
        {
            carActive = false;
            mainCamera.gameObject.SetActive(true);
        }
        //summoning
        if (Input.GetKeyDown(KeyCode.JoystickButton5) && !carActive && canSummon && !MoveFox.dialogStop && !MoveFox.jmp && !MoveFox.falling && !MoveFox.ELEV && !PlayerAttack.press)
        {
            Instantiate(rangeControlCar,SpPos.transform.position,SpPos.transform.rotation);
            rangeControlCar.transform.position = SpPos.transform.position;
            carActive = true;   
            summons += 1;
            canSummon = false;
        }
    }
    //raycast creating
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        return Raycast(offset, rayDirection, length, objectLayer);
    }
    //raycast example
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);
    
        if (drawDebugRaycasts)
        {
            Color color = hit ? Color.red : Color.green;
         
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }
        return hit;
    }
}
