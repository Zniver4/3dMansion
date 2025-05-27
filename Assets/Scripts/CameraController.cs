using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Referencia al objeto jugador que la c�mara debe seguir.")]
    public Transform player; 

    [Tooltip("Habilite para mover la c�mara manteniendo presionado el bot�n derecho del mouse. No funciona con joysticks.")]
    public bool clickToMoveCamera = false;
    [Tooltip("Habilite acercar/alejar al desplazar la rueda del mouse. No funciona con joysticks.")]
    public bool canZoom = true;
    [Space]
    [Tooltip("Cuanto m�s alto es, m�s r�pido se mueve la c�mara. Se recomienda aumentar este valor para juegos que utilizan joystick.")]
    public float sensitivity = 5f;

    [Tooltip("L�mites de rotaci�n de la c�mara Y. El eje X es lo m�ximo que puede subir y el eje Y es lo m�ximo que puede bajar.")]
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
                Debug.LogWarning("No se encontr� una c�mara con el tag 'MainCamera'.");
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

