using System;
using System.Collections.Generic;
using System.Transactions;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]
public class GunController : MonoBehaviour
{
    InputAction attackAction;

    public enum FiringState
    {
        None,
        Idle,
        Firing,
        Reloading,
        Preparing
    }

    public FiringState state = FiringState.None;

    [Header("Outside Objects")]
    private GameManagement gameManager;

    [Header("Gameplay Variables")]
    public float firingDelay;
    public float reloadTime = 0;
    private float nextFirePoint = 0;
    public int magSize = 1; // only utilized by missiles so far
    public int shotsRemaining = 1;
    public int burstSize; // only utilized by missiles so far
    public int ammoPerRound = 10;
    public int ammoLeft = 10;
    public GameObject ammoObject;
    private ProjectileBehavior ammoBehavior;
    public float speedRot = 0.5f; // less then or equal to 1
    public GameObject persistentProjectile;

    [Header("Personal Rotational Variables")]
    public float targetAngle; // the "goal" angle
    public float currentAngle; // easier to work with then transform.rotation.z
    public Vector3 targetPos; // the target, as cordinates
    public Vector3 directionVec; // the target as a normalized vector

    [Header("Personal Display Variables")]
    public Animator animator;
    public bool isAnimationPlaying;
    public bool isAnimationSingleShot; // check if the animation is per shot, or if it shows multiple projectiles
    public Sprite displayImage;
    public int descriptionID;  // tells the inventory what description to show
    public string description;
    public string weaponName;
    public string animationName;
    public GameObject targetingIndicator; // marks the target of projectiles

    private AudioSource audioSource;
    private DataManagement saveData;

    private bool isPrepairing;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // assig variables
        gameManager = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0];
        shotsRemaining = magSize;

        // assign actions
        attackAction = InputSystem.actions.FindAction("Attack");

        ammoBehavior = ammoObject.GetComponent<ProjectileBehavior>();
        audioSource = gameObject.GetComponent<AudioSource>();
        saveData = FindObjectsByType<GameManagement>(FindObjectsSortMode.None)[0].GetComponent<DataManagement>();
        animator = GetComponent<Animator>();

        if (attackAction == null)
        {
            Debug.Log("Attack action not assigned");
        }
    }


    // Update is called once per frame
    void Update()
    {


        if (state == FiringState.Preparing || isPrepairing)
        {
            if (nextFirePoint < Time.time)
            {
                state = FiringState.Idle;
                isPrepairing = false;
            }
            //transform.position = new Vector3(5, 5, 5);
            
        }
        else
        {
            // don't target if the game is paused
            if (gameManager.paused == false)
            {
                Targeting();
            }


            if (shotsRemaining <= 0 && nextFirePoint < Time.time)
            {
                shotsRemaining = magSize;
                state = FiringState.Idle;
            }
            else if (nextFirePoint <= Time.time && attackAction.IsPressed())
            {
                state = FiringState.Firing;

                if (!isAnimationPlaying && ammoLeft > 0)
                {
                    animator.Play(animationName);
                }

                // don't shoot if the game is paused 
                if (gameManager.paused == false)
                {
                    if (nextFirePoint <= Time.time && attackAction.IsPressed() && ammoLeft > 0)
                    {
                        if (ammoBehavior.type == ProjectileBehavior.MunitionType.Basic)
                        {
                            FireBasic();
                        }
                        if (ammoBehavior.type == ProjectileBehavior.MunitionType.Explosive)
                        {
                            FireExplosive();
                        }
                        if (ammoBehavior.type == ProjectileBehavior.MunitionType.Laser)
                        {
                            FireLaser();
                        }
                        if (ammoBehavior.type == ProjectileBehavior.MunitionType.Missile)
                        {
                            FireMissile();
                        }
                        if (ammoBehavior.type == ProjectileBehavior.MunitionType.Arcing)
                        {
                            FireArcing();
                        }
                    }
                    else
                    {
                        if (ammoLeft < 0)
                        {
                            ammoLeft = 0;
                        }
                    }

                }
            }
        
        

            if (!isAnimationSingleShot)
            {
                if (shotsRemaining > 0 && !attackAction.IsPressed())
                {
                    animator.speed = 0;
                }
                else
                {
                    animator.speed = 1;
                }
            }
            // reset missile if the player lets go of the mouse
            if (!attackAction.IsPressed() && ammoBehavior.type == ProjectileBehavior.MunitionType.Missile && false)
            {
                if (burstSize > 0)
                {
                    nextFirePoint = Time.time + firingDelay;
                }
                burstSize = 0;
            }

            // advanced laser logic
            if (ammoBehavior.type == ProjectileBehavior.MunitionType.Laser)
            {
                if (attackAction.IsPressed() && ammoLeft > 0)
                {
                    if (persistentProjectile == null)
                    {
                        persistentProjectile = Instantiate(ammoObject, transform.position, transform.rotation);
                    }
                    persistentProjectile.GetComponent<ProjectileBehavior>().targetLength = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
                    persistentProjectile.transform.rotation = transform.rotation;
                }
                else
                {
                    if (persistentProjectile != null)
                    {
                        Destroy(persistentProjectile);
                    }
                }
            }


            // state management:
            if (nextFirePoint <= Time.time)
            {
                if (attackAction.IsPressed())
                {
                    state = FiringState.Firing;
                }
                else
                {
                    if (state != FiringState.Reloading)
                    {
                        state = FiringState.Idle;
                    }
                }
            }
        }
    }
    void FireBasic()
    {
        GameObject clone = Instantiate(ammoObject, transform.position + Vector3.forward, transform.rotation);
        clone.transform.position = transform.position + Vector3.forward;
        clone.GetComponent<Rigidbody2D>().linearVelocity = directionVec * 10;
        nextFirePoint = Time.time + firingDelay;
        audioSource.Play();

        shotsRemaining -= 1;
        ammoLeft -= 1;
        CheckMag();
    }
    void FireExplosive()
    {
        GameObject clone = Instantiate(ammoObject, transform.position, transform.rotation);
        clone.transform.position = transform.position + Vector3.forward;
        clone.GetComponent<Rigidbody2D>().linearVelocity = directionVec * 10;
        nextFirePoint = Time.time + firingDelay;

        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos = new Vector3(targetPos.x, targetPos.y, 1); // so that the indicator isnt at the same z position as the camera
        clone.GetComponent<ProjectileBehavior>().targetPosition = targetPos;
        clone.GetComponent<ProjectileBehavior>().targetIndicator = Instantiate(targetingIndicator, targetPos, transform.rotation);
        audioSource.Play();

        shotsRemaining -= 1;
        ammoLeft -= 1;
        CheckMag();
    }
    void FireLaser()
    {
        if (persistentProjectile != null)
        {
            persistentProjectile.GetComponent<ProjectileBehavior>().damagePulse = true;
            persistentProjectile.GetComponent<ProjectileBehavior>().startingPosition = transform.position;
        }
        nextFirePoint = Time.time + firingDelay;

        shotsRemaining -= 1;
        if (Time.time % 0.25 <= 0.01f)
        {
            ammoLeft -= 1;
        }
        CheckMag();
    }
    void FireMissile()
    {
        if (burstSize < magSize)
        {
            GameObject clone = Instantiate(ammoObject, transform.position, transform.rotation);
            clone.transform.position = transform.position + Vector3.forward;
            clone.GetComponent<Rigidbody2D>().linearVelocity = directionVec * 10;

            burstSize++;
            ammoLeft -= 1;
            nextFirePoint = Time.time + 0.1f;
        }
        else
        {
            burstSize = 0;
            nextFirePoint = Time.time + firingDelay;
        }
    }
    void FireArcing()
    {
        if (persistentProjectile == null)
        {
            persistentProjectile = Instantiate(ammoObject, transform.position, transform.rotation);
            persistentProjectile.transform.position = transform.position + Vector3.forward;
            persistentProjectile.GetComponent<ProjectileBehavior>().targetIndicator = Instantiate(targetingIndicator, transform.position, transform.rotation);
            nextFirePoint = Time.time + firingDelay;
            audioSource.Play();

            shotsRemaining -= 1;
            ammoLeft -= 1;
            CheckMag();
        }
    }

    void CheckMag()
    {
        if (shotsRemaining <= 0)
        {
            nextFirePoint = Time.time + reloadTime;
            state = FiringState.Reloading;
        }
    }

    void Targeting()
    {

        // point arcing gun at persisting projectile
        if (ammoBehavior.type == ProjectileBehavior.MunitionType.Arcing)
        {
            transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            //transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;

            // the current screen position of the mouse
            targetPos = Input.mousePosition;
            targetPos.z = 5.23f;

            // tranlates the screen position to world position
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            targetPos.x = targetPos.x - objectPos.x;
            targetPos.y = targetPos.y - objectPos.y;

            directionVec = targetPos.normalized;

            // find target angle
            targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - currentAngle;

            // stops overshooting
            if (Mathf.Abs(targetAngle) > 180)
            {
                currentAngle = currentAngle * -1;
                targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - currentAngle; // I made this awhile ago, its black magic to me now
            }

            // start rotating
            currentAngle = currentAngle + (targetAngle) * speedRot;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));

        }
    }
    public void WakeUp()
    {
        //isPrepairing = true;
        state = FiringState.Preparing;
        transform.parent.GetComponent<Animator>().Play("SwivelSwap");
        nextFirePoint = Time.time + 1;
        Debug.Log(nextFirePoint);
    }
}