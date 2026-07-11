using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    public float speed = 2f;
    public float stayTime = 1f;
    
    private Vector3 startPosition;
    public Player PlayerScript;
    
    public Animator Animator;
    public int reward = 100;
    bool canBeKilled = false;
    
    
    private bool isDead = false;
    public EnemyState state = EnemyState.Idle;
    public enum  EnemyState
    {
        Idle,
        Walking,
        wait,
        Attacking,
        Death
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Keyboard.current.jKey.wasPressedThisFrame && state == EnemyState.Idle)
        {
            state = EnemyState.Walking;
            Animator.SetBool("Walk", true);
            StartCoroutine(Attack());
        }
        float distance = Vector3.Distance(transform.position, Player.position);
        if (canBeKilled && !isDead && distance < 4f && Mouse.current.leftButton.wasPressedThisFrame)
        {
            state = EnemyState.Death;
            Animator.SetTrigger("Dead");
            killEnemy();
        }
        if (canBeKilled && !isDead && distance < 2f && Mouse.current.rightButton.wasPressedThisFrame)
        {
            state = EnemyState.Death;
            Animator.SetTrigger("Dead");
            killEnemy();
        }
        
    }

    void killEnemy()
    {
        state = EnemyState.Death;
        isDead = true;
        StopAllCoroutines();
        Animator.SetTrigger("Death");
        PlayerScript.AddScore(reward);
        enabled = false;
    }
    
    IEnumerator Attack()
    {
        while (Vector3.Distance(transform.position, Player.position) > 0.1f)
        {
            state = EnemyState.Attacking;
            Animator.SetTrigger("punch");
            transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
            yield return null;
            
        }

        state = EnemyState.wait;
        Animator.SetBool("Walk", false);
        
        canBeKilled = true;
        Animator.SetTrigger("Punch");
        yield return new WaitForSeconds(0.35f);
        PlayerScript.RemoveSoul();
        yield return new WaitForSeconds(stayTime);
        
        canBeKilled = false;
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            state = EnemyState.Walking;
            Animator.SetBool("Walk", false);
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            yield return null;
        }

        state = EnemyState.Idle;
    }
   
    }
