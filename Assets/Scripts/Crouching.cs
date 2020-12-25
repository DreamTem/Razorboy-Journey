using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Crouching : MonoBehaviour
{
    public bool drawDebugRaycasts = true;
    public LayerMask groundLayer;
    public float groundDistance = .2f;
    public static bool isCrouching,isHeadBlocked;
    BoxCollider2D bodyCollider;
    float originalXScale;          //Size of the crouching collider
    float playerHeight,crouch;
    public Animator anim;
    //collider sizes
    Vector2 colliderStandSize;              //Size of the standing collider
    Vector2 colliderStandOffset;            //Offset of the standing collider
    Vector2 colliderCrouchSize;             //Size of the crouching collider
    Vector2 colliderCrouchOffset;
    // Start is called before the first frame update
    void Start()
    {
        isHeadBlocked = false;
        bodyCollider = GetComponent<BoxCollider2D>();
        //normal collider
        playerHeight = bodyCollider.size.y;
        colliderStandSize = bodyCollider.size;
        colliderStandOffset = bodyCollider.offset;
        originalXScale = transform.localScale.x;
        //crouch collider
        colliderCrouchSize = new Vector2(bodyCollider.size.x, bodyCollider.size.y / 2f);
        colliderCrouchOffset = new Vector2(bodyCollider.offset.x, bodyCollider.offset.y -0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        //gamepad crouch axis
        crouch = Input.GetAxis("JumpCrouch");
        //checking head block, when crouching
        RaycastHit2D headCheck = Raycast(new Vector2(0, 0.38f), Vector2.up, groundDistance);
        //head blocking
        if (headCheck)
        {
            isHeadBlocked = true;
        }
        if(crouch == -1 && !MoveFox.rn && !isHeadBlocked && !MoveFox.dialogStop && !CarSummon.carActive && !MoveFox.jmp && !MoveFox.falling && !MoveFox.ELEV)
        {
            Invoke("Crouch",0.15f);
        }
        //staning up
        if (crouch == 0 && isCrouching)
        {
            NormalSize();
        }
    }
    //crouching method
     public void Crouch()
    {
            //crouching enter
            anim.SetBool("CRCH", true);
            MoveFox.rn = false;
            MoveFox.running = false;
            isCrouching = true;
            bodyCollider.size = colliderCrouchSize;
            bodyCollider.offset = colliderCrouchOffset;
    }
    //first raycast creating
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        return Raycast(offset, rayDirection, length, groundLayer);
    }
    //main raycast example
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


    //resize to normal
    void NormalSize()
    {
        if (isHeadBlocked)
        {
            return;
        }
        anim.SetBool("CRCH", false);
        isCrouching = false;
        bodyCollider.size = colliderStandSize;
        bodyCollider.offset = colliderStandOffset;
    }
}
