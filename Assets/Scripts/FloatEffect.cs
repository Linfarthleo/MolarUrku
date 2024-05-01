using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    public float amplitude = 0.1f; // La altura máxima de la oscilación
    public float frequency = 0.5f; // La velocidad de la oscilación

    private Vector3 startPos; // Posición inicial del objeto

    void Start()
    {
        startPos = transform.position; // Guardar la posición inicial
    }

    void Update()
    {
        // Calcula la nueva posición del objeto
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        // Aplica la nueva posición
        transform.position = tempPos;
    }
}
