using UnityEngine;
using UnityEngine.InputSystem;

public class PingPong : MonoBehaviour
{
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.numpad1Key.wasPressedThisFrame)
            animator.SetBool("PingPong", true);
        
        if (Keyboard.current.numpad2Key.wasPressedThisFrame)
            animator.SetTrigger("Shoot");
            
        if (Keyboard.current.numpad3Key.wasPressedThisFrame)
            animator.SetBool("TriangleMoves", true);

        if (Keyboard.current.numpad4Key.wasPressedThisFrame)
            animator.SetTrigger("Kick");
        if (Keyboard.current.numpad5Key.wasPressedThisFrame)
            animator.SetBool("Move", true);
    }
}
