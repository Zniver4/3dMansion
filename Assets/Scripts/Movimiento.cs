using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public float velocidadCorrer = 10f;
    public float fuerzaSalto = 7f;
    private Rigidbody rb;
    private bool enSuelo = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento horizontal
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        // Correr si se mantiene presionado Shift
        float velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidad;

        Vector3 movimiento = new Vector3(movimientoX, 0, movimientoZ) * velocidadActual * Time.deltaTime;
        transform.Translate(movimiento, Space.World);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detecta si el personaje está en el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            enSuelo = true;
        }
    }
}
