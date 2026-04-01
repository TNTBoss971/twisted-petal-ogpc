using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D sceneCursor;

    public bool useCrosshair;
    public GameObject crosshair;

    public Camera cameraTemp;
    private GameManagement gameManager;
    public GunController activeWeapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Cursor.SetCursor(sceneCursor, Vector3.zero, CursorMode.ForceSoftware);
       gameManager = GameObject.Find("GameManager").GetComponent<GameManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (useCrosshair)
        {
            // get weapon
            activeWeapon = gameManager.equippedWeapons[gameManager.activeWeaponId].GetComponent<GunController>();


            // Get the mouse position from the Input system (legacy or new)
            Vector3 mouseScreenPosition = Input.mousePosition;

            // Set the Z value to a distance in front of the camera (e.g., near clip plane)
            // For 2D, you might want Z=0 after conversion, but you need depth for the conversion itself
            mouseScreenPosition.z = cameraTemp.nearClipPlane;


            crosshair.transform.position = cameraTemp.ScreenToWorldPoint(mouseScreenPosition);

            // animation
            if (activeWeapon.state == GunController.FiringState.Firing)
            {
                crosshair.GetComponent<Animator>().Play("FireCrosshair");
            }
            if (activeWeapon.state == GunController.FiringState.Reloading)
            {
                crosshair.GetComponent<Animator>().Play("ReloadingCrosshair");
            }
            if (activeWeapon.state == GunController.FiringState.Idle)
            {
                crosshair.GetComponent<Animator>().Play("IdleCrosshair");
            }
        }
    }
}
