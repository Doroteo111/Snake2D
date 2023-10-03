using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridPosition;
    private Vector2Int startGridPosition;
    private Vector2Int gridMoveDirection;

    private float horizontalInput, verticalInput;

    private float gridMoveTimer;
    private float gridMoveTimerMax = 1f; // La serpiente se moverá a cada segundo

    private void Awake()
    {
        startGridPosition = new Vector2Int(0, 0);
        gridPosition = startGridPosition;

        gridMoveDirection = new Vector2Int(0, 1); // Dirección arriba por defecto
    }

    private void Update()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimerMax;

            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
        }
    }
    private void HandleMoveDirection()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Cambio dirección hacia arriba
        if (verticalInput > 0) // Si he pulsado hacia arriba (W o Flecha Arriba)
        {
            if (gridMoveDirection.y != 1) // Si iba en horizontal
            {
                // Cambio la dirección hacia arriba
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
            }
        }

        // Cambio dirección hacia abajo

        // Cambio dirección hacia derecha

        // Cambio dirección hacia izquierda
    }
}
}
