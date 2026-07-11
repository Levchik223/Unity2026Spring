using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float xStep = 1f;
    public float yStep = 1f;
    public float speed = 1f;
    public float RotateAngle = 0.1f;
    private Vector3 originalScale;
    public int Score = 0;
    public HUD hud;
    
    
    private SpriteRenderer sr;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int horizontal;
    private int vertical;
    
    String PunchAnimation = "PlayerPunch";
    string animationKick = "PlayerKick";
    public int Health = 100;
    public int MaxHealth = 100;
    public int Souls = 5;
    public GameObject gameOverScreen;
    
    public Collider2D WalkableArea;
    public TMP_Text scoreText;
    
    
    public void RemoveSoul()
    {
        if (Souls > 0)
        {
            Souls--;
            if(hud)
                hud.UpdateSouls(Souls); 
            if (Souls == 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        gameOverScreen.SetActive(true);
        GetComponent<Player>().enabled = false;
    }
   

    public void AddScore(int Amount)
    {
        Score += Amount;
        scoreText.text = "Score: " + Score;
       // scoreText.text = Score.ToString();
        if (hud) 
            hud.UpdateScore(Score);
    }

    void AddHp(int Amount)
    {
        Health += Amount;
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        Debug.Log("health: " + Health);
    }

    void TakeDamage(int Amount)
    {
        AddHp (-Amount);
        if (Health <= 0)
        {
            Souls--;
            Health = MaxHealth;
            if (hud)
                hud.UpdateSouls(Souls);
        }
    }

   

    void flipCharacter(bool flip)
    {
        if (flip)
        {
            transform.localScale = new Vector3(
                -originalScale.x,
                originalScale.y, 
                originalScale.z);
        }
        else
            transform.localScale = new Vector3 (
                originalScale.x, 
                originalScale.y, 
                originalScale.z);
    }
  
   
    

    bool isAnimationFinished(string animationName)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName(animationName))
            return true;
        if (info.normalizedTime > 0.95f)
            return true;
        return false;
    }
        

    void moveCharacter(int Horizontal, int Vertical)
    {
        if (Horizontal == 0 && Vertical == 0)
        {
            animator.SetBool("IsWalking", false);
            if (Health > 20) animator.SetBool("IsDying", false);
            else animator.SetBool("IsDying", true);
            return;
        }
        animator.SetBool("IsWalking", true);
        
        Vector3 Offset = Vector3.zero;
        
        if (Horizontal > 0)
            Offset = Vector3.right * xStep * speed * Time.deltaTime;
        

        if (Horizontal < 0)
            Offset = Vector3.left * xStep * speed * Time.deltaTime;
        

        if (Vertical > 0)
            Offset = Vector3.up * yStep * speed * Time.deltaTime;
        

        if (Vertical < 0)
            Offset = Vector3.down * yStep * speed * Time.deltaTime;
     
        Debug.Log(Offset);
        
        Vector3 TargetPosition = transform.position + Offset;
        
        if (CanMove(Offset, TargetPosition))
           transform.Translate(Offset);
    }

    private bool CanMove(Vector3 Offset, Vector3 TargetPosition)
    {
        return Offset != Vector3.zero && WalkableArea.OverlapPoint(TargetPosition);
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
           originalScale = transform.localScale;
           InitPlayerData();
    }

    private void InitPlayerData()
    {
        if (hud)
        {
            hud.UpdateScore(0);
            hud.UpdateSouls(Souls);
        }
    }

    void Start()
    {
        gameOverScreen.SetActive(false);
    }
 

    // Update is called once per frame
    void Update()
    {
        /* if(Mouse.current.leftButton.isPressed)
             sr.color = Color.blueViolet;
         else if (Mouse.current.leftButton.wasReleasedThisFrame)
             sr.color = Color.red; */

        /* if (Mouse.current.scroll.ReadValue().y > 0)
             Debug.Log("scroll up");
         if (Mouse.current.scroll.ReadValue().y < -0)
             Debug.Log("scroll down"); */

        //Debug.Log(Mouse.current.position.ReadValue());
        

       
    if (Keyboard.current.kKey.wasPressedThisFrame)
       {
          RemoveSoul();
       }
        
        bool isWASD_pressed = false;
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //TakeDamage(10);
            animator.Play("PlayerKick");
        }
        if (!isAnimationFinished("playerKick"))
            return; 
        
      
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //AddScore(10);
            animator.Play("PlayerPunch");
        }
        if (!isAnimationFinished("PlayerPunch"))
            return;
        
        if (Keyboard.current.dKey.isPressed)
        {
            flipCharacter(false);
            moveCharacter(1, 0);
            isWASD_pressed = true;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            flipCharacter(true);
            moveCharacter(-1, 0);
            isWASD_pressed = true;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            moveCharacter(0, 1);
            isWASD_pressed = true;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            moveCharacter(0, -1);
            isWASD_pressed = true;
        }
        if(!isWASD_pressed) moveCharacter(0, 0);
        
    
        
       /* Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;
        Vector3 direction = mouseWorldPosition - transform.position;
        if (direction.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else 
        {
            transform.eulerAngles = new Vector3(0, 0, -180);
        } */
       
       



        // transform.Rotate(new Vector3(0, 0, 1), RotateAngle);
    } 
}

