using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class recogermonedas : MonoBehaviour
{
    public int cantidadMonedas;
    public TextMeshProUGUI numero;

    private void Update()
    {
        numero.text = cantidadMonedas.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moneda"))
        {
            Moneda moneda = other.GetComponent<Moneda>();
            int valor = moneda != null ? moneda.valor : 1; 
            cantidadMonedas += valor;
            Destroy(other.gameObject);
        }
    }
}
