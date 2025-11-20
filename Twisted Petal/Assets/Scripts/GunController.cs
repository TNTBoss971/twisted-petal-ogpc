using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    InputAction attackAction;
    /*
    [Header("Outside Objects")]
    public GameManagement gameManager;
    */

    [Header("Gameplay Variables")]
    public float firingDelay;
    private float nextFirePoint = 0;
    public GameObject ammoObject;
    public ProjectileBehavior ammoBehavior;
    public float speedRot = 0.5f; // less then or equal to 1

    [Header("Personal Rotational Variables")]
    public float targetAngle; // the "goal" angle
    public float currentAngle; // easier to work with then transform.rotation.z
    public Vector3 targetPos; // the target, as cordinates
    public Vector3 directionVec; // the target as a normalized vector

    [Header("Personal Display Variables")]
    public Sprite displayImage;
    public int descriptionID;  // tells the inventory what description to show


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // assign actions
        attackAction = InputSystem.actions.FindAction("Attack");

    }

    // Update is called once per frame
    void Update()
    {
        Targeting();


        if (nextFirePoint <= Time.time && attackAction.IsPressed())
        {
            if (ammoBehavior.type == ProjectileBehavior.MunitionType.Basic)
            {
                FireBasic();
            }
            else if (ammoBehavior.type == ProjectileBehavior.MunitionType.Explosive)
            {
                FireExplosive();
            }
        }
    }

    void FireBasic()
    {
        GameObject clone = Instantiate(ammoObject, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody2D>().linearVelocity = directionVec * 10;
        nextFirePoint = Time.time + firingDelay;
    }
    void FireExplosive()
    {
        GameObject clone = Instantiate(ammoObject, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody2D>().linearVelocity = directionVec * 10;
        nextFirePoint = Time.time + firingDelay;
        clone.GetComponent<ProjectileBehavior>().targetPosition = targetPos;
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
