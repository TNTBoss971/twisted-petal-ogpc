using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class GunController : MonoBehaviour
{
    InputAction attackAction;
    /*
    [Header("Outside Objects")]
    public GameManagement gameManager;
    */

    [Header("Gameplay Variables")]
    public float firingDelay;
    public float reloadTime = 0;
    private float nextFirePoint = 0;
    public int magSize = 1; // only utilized by missiles so far
    public int shotsRemaining = 1;
    public int burstSize; // only utilized by missiles so far
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
    public GameObject targetingIndicator; // marks the target of projectiles

    private AudioSource audioSource;
    private DataManagement saveData;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        // if sfx muted, volume is set to 0
        if (saveData.soundsMuted == true)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Targeting();

        if (shotsRemaining <= 0 && nextFirePoint > Time.time)
        {
            shotsRemaining = magSize;   
        }
        else if (nextFirePoint <= Time.time && attackAction.IsPressed())
        {
            if (!isAnimationPlaying)
            {
                animator.Play(weaponName + "Fire");
            }

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
            if (attackAction.IsPressed())
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
    }

    void FireBasic()
    {
        GameObject clone = Instantiate(ammoObject, transform.position + Vector3.forward, transform.rotation);
        clone.GetComponent<Rigidbody2D>().linearVelocity = directionVec * 10;
        nextFirePoint = Time.time + firingDelay;
        audioSource.Play();

        shotsRemaining-=1;
        CheckMag();
    }
    void FireExplosive()
    {
        GameObject clone = Instantiate(ammoObject, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody2D>().linearVelocity = directionVec * 10;
        nextFirePoint = Time.time + firingDelay;

        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos = new Vector3(targetPos.x, targetPos.y, 1); // so that the indicator isnt at the same z position as the camera
        clone.GetComponent<ProjectileBehavior>().targetPosition = targetPos;
        clone.GetComponent<ProjectileBehavior>().targetIndicator = Instantiate(targetingIndicator, targetPos, transform.rotation);
        audioSource.Play();

        shotsRemaining-=1;
        CheckMag();
    }
    void FireLaser()
    {
        if (persistentProjectile != null)
        {
            persistentProjectile.GetComponent<ProjectileBehavior>().damagePulse = true;
        }
        nextFirePoint = Time.time + firingDelay;

        shotsRemaining-=1;
        CheckMag();
    }
    void FireMissile()
    {
        if (burstSize < magSize)
        {
            GameObject clone = Instantiate(ammoObject, transform.position, transform.rotation);
            clone.GetComponent<Rigidbody2D>().linearVelocity = directionVec * 10;

            burstSize++;
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
            persistentProjectile.GetComponent<ProjectileBehavior>().targetIndicator = Instantiate(targetingIndicator, transform.position, transform.rotation);
            nextFirePoint = Time.time + firingDelay;
            audioSource.Play();

            shotsRemaining-=1;
            CheckMag();
        }
    }

    void CheckMag()
    {
        if (shotsRemaining <= 0)
        {
            nextFirePoint = Time.time + reloadTime;
        }
    }

    void Targeting() {

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
