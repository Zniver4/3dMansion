using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Referencia al objeto jugador que la cámara debe seguir.")]
    public Transform player; 

    [Tooltip("Habilite para mover la cámara manteniendo presionado el botón derecho del mouse. No funciona con joysticks.")]
    public bool clickToMoveCamera = false;
    [Tooltip("Habilite acercar/alejar al desplazar la rueda del mouse. No funciona con joysticks.")]
    public bool canZoom = true;
    [Space]
    [Tooltip("Cuanto más alto es, más rápido se mueve la cámara. Se recomienda aumentar este valor para juegos que utilizan joystick.")]
    public float sensitivity = 5f;

    [Tooltip("Límites de rotación de la cámara Y. El eje X es lo máximo que puede subir y el eje Y es lo máximo que puede bajar.")]
    public Vector2 cameraLimit = new Vector2(-45, 40);

    float mouseX;
    float mouseY;
    float offsetDistanceY;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("No se ha asignado el jugador en el inspector.");
            enabled = false;
            return;
        }
        offsetDistanceY = transform.position.y;

        if (!clickToMoveCamera)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (canZoom && Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Camera.main != null)
            {
                Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * sensitivity * 2;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 20f, 100f);
            }
            else
            {
                Debug.LogWarning("No se encontró una cámara con el tag 'MainCamera'.");
            }
        }
        transform.position = player.position + new Vector3(0, offsetDistanceY, 0);

        if (canZoom && Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * sensitivity * 2;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 20f, 100f);
        }

        if (clickToMoveCamera && !Input.GetMouseButton(1))
            return;

        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);

        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
    }
}

