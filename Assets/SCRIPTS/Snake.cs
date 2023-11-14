using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private enum Direction //nuevo tipo de variale --> enumerado
    {
        Left,
        Right, 
        Down,
        Up
    }
    private enum State
    {
        Alive,
        Death
    }

    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition; // Posición 2D de la SnakeBodyPart
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyPartGameObject = new GameObject("Snake Body",
                typeof(SpriteRenderer));
            SpriteRenderer snakeBodyPartSpriteRenderer = snakeBodyPartGameObject.GetComponent<SpriteRenderer>();
            snakeBodyPartSpriteRenderer.sprite =
                GameAssets.Instance.snakeBodySprite;
            snakeBodyPartSpriteRenderer.sortingOrder = -bodyIndex;
            transform = snakeBodyPartGameObject.transform;
        }

        public void SetMovePosition(SnakeMovePosition snakeMovePosition)    //actualiza posicion2d y 3d
        {
            // Posición (gridPosition)
            this.snakeMovePosition = snakeMovePosition; // Posición 2D y la dirección de la SnakeBodyPart
            Vector2Int gridPosition = snakeMovePosition.GetGridPosition();
            transform.position = new Vector3(gridPosition.x,
                gridPosition.y, 0); // Posición 3D del G.O.

            //Dirección 
            float angle; //esto determinara la rotación  
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Left:
                    angle = 90;
                    break;
                case Direction.Right:
                    angle = -90;
                    break;
                case Direction.Up:
                    angle = 0;
                    break;
                case Direction.Down:
                    angle = 180;
                    break;
            } // Fin del switch
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    private class SnakeMovePosition //saber la direccion que tiene que mirar cada parte del cuerpo //constructor
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction)
        {
            //necesito que las lean
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition ()
        {
            return gridPosition;
        }
        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            return previousSnakeMovePosition.GetDirection();
        }
    }

    private Vector2Int gridPosition; //posicion cabeza 2d
    private Vector2Int startGridPosition;
    //private Vector2Int gridMoveDirection; //direccion de la cabeza
    private Direction gridMoveDirection;

    private float horizontalInput, verticalInput;

    private float gridMoveTimer;
    [SerializeField]private float gridMoveTimerMax = 0.5f; // La serpiente se moverá a cada segundo

    private LevelGrid levelGrid;

    private int snakeBodySize;
    // private List<Vector2Int> snakeMovePositionsList; //posiciones parte del cuerpo (por orden) CAMBIA ABAJO
    private List<SnakeMovePosition> snakeMovePositionsList; //posicion 2d + direccion a la que mirar
    private List<SnakeBodyPart> snakeBodyPartsList; //lista snakeBoyPart

    private State state;

    private void Awake()
    {
        startGridPosition = new Vector2Int(0, 0);
        gridPosition = startGridPosition;

        gridMoveDirection = Direction.Up; // Dirección arriba por defecto
        transform.eulerAngles = Vector3.zero; // Rotación arriba por defecto

        snakeBodySize = 0;
        snakeMovePositionsList = new List<SnakeMovePosition>();
        snakeBodyPartsList=new List<SnakeBodyPart>(); //crear lista vacia

        state = State.Alive;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Alive:                //si estoy vivo
                HandleMoveDirection();
                HandleGridMovement();
                break;
            case State.Death:                //la serpiete no se mueve
                break;
        }
        
    }

    public void Setup(LevelGrid levelGrid)
    {
        // levelGrid de snake = levelGrid que viene por parámetro
        this.levelGrid = levelGrid;
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime; //reinicia el tiempo
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            SoundManager.PlaySound(SoundManager.Sound.SnakeMove);//*****************************************************************

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionsList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionsList[0];
            }


            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection); // direccion y posiciond de la cabeza
            snakeMovePositionsList.Insert(0, snakeMovePosition);
            //enchufando la ultima posicion actual se guarda en la lista--> pasa a ser la primera parte cu

            //realacion entre direcones y vectores, left,right,down,up
            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Direction.Left:
                    gridMoveDirectionVector = new Vector2Int(-1, 0);
                    break;
                case Direction.Right:
                    gridMoveDirectionVector = new Vector2Int(1, 0);
                    break;
                case Direction.Down:
                    gridMoveDirectionVector = new Vector2Int(0, -1);
                    break;
                case Direction.Up:
                    gridMoveDirectionVector = new Vector2Int(0,1);
                    break;

            }

            gridPosition += gridMoveDirectionVector; 
            gridPosition= levelGrid.ValidateGridPosition(gridPosition);
            //si me he comido una parte de mi cuerpo

            foreach (SnakeMovePosition movePosition in snakeMovePositionsList)
            {
             //si coincide con cada una de las partes
             if (gridPosition == movePosition.GetGridPosition())
                {
                    //GameOver
                    state = State.Death;
                    GameManager.Instance.SnakeDied(); //*******************************************************
                }
            }

            // ¿He comido comida?
            bool snakeAteFod = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFod)
            {
                //cuerpo crece
                snakeBodySize++;
                //llamada la función
                CreateBodyPart();
               Score.AddScore(Score.POINTS);
                
            }

            if (snakeMovePositionsList.Count > snakeBodySize) //elimina la coordenada restante
            {
                snakeMovePositionsList.
                    RemoveAt(snakeMovePositionsList.Count - 1);
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector));

            UpdateBodyParts(); 
        
        }
    }

    private void HandleMoveDirection() //relativo al movimeitno 2D
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Cambio dirección hacia arriba
        if (verticalInput > 0) // Si he pulsado hacia arriba (W o Flecha Arriba)
        {
            if (gridMoveDirection != Direction.Down) // Si iba en horizontal
            {
                // Cambio la dirección hacia arriba (0,1)
                gridMoveDirection = Direction.Up;
            }
        }

        // Cambio dirección hacia abajo
        // Input es abajo?
        if (verticalInput < 0)
        {
            // Mi dirección hasta ahora era horizontal
            if (gridMoveDirection != Direction.Up)
            {
                gridMoveDirection = Direction.Down;
            }
        }

        // Cambio dirección hacia derecha
        if (horizontalInput > 0)
        {
            if (gridMoveDirection != Direction.Left)
            {
                gridMoveDirection = Direction.Right;
            }
        }

        // Cambio dirección hacia izquierda
        if (horizontalInput < 0)
        {
            if (gridMoveDirection != Direction.Right)
            {
                gridMoveDirection = Direction.Left;
            }
        }
    }

    private float GetAngleFromVector(Vector2Int direction)
    {
        float degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (degrees < 0)
        {
            degrees += 360;
        }

        return degrees - 90;
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public List<Vector2Int> GetFullSnakeBodyGridPosition() 
    {
        //añademe un conjunto de elementos y luego la devuelves
        List<Vector2Int> gridPositionList=new List<Vector2Int>() { gridPosition }; //lista de todas las posiciones cabeza+cuerpo
        foreach(SnakeMovePosition snakeMovePosition in snakeMovePositionsList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition()); // de todas las partes, una por una, pillas la posicion
        }
        
        return gridPositionList;
    } 

    private void CreateBodyPart() //me cuenta cuantas partes del cuerpo tengo, se llama siempre q come comida
    {
        //cabeza -->0, cuerpo -->-1,-2,-3,...
        snakeBodyPartsList.Add(new SnakeBodyPart(snakeBodySize)); 
    }
    
    private void UpdateBodyParts() //cada vez qyue me muevo necesito actualizarlo
    {
        for(int i = 0; i<snakeBodyPartsList.Count; i++)
        {
            snakeBodyPartsList[i].SetMovePosition(snakeMovePositionsList[i]); //solo quiero la posicion
        }
    }
}

