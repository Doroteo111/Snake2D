using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;

    private int width;
    private int height;

    private Snake snake;

    public LevelGrid(int w, int h)
    {
        width = w;
        height = h;
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) //lo usabamos si la serpiente habia comido fruta
    {
        if (snakeGridPosition == foodGridPosition) //come fruta
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpawnFood()
    {
        // while (condicion){
        // cosas
        // }

        // { cosas }
        // while (condicion)

        do
        {
            foodGridPosition = new Vector2Int(
                Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));

           // acceso a la lista, dentro busca y calcula una posion donde no coincida con la
           // serpiente para instanciar ahí una fruta, si no la encuentra re calcula
        } while (snake.GetFullSnakeBodyGridPosition().IndexOf(foodGridPosition) != -1); 

        foodGameObject = new GameObject("Food");
        SpriteRenderer foodSpriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = GameAssets.Instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y, 0);
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        int w = Half(width);
        int h = Half(height);

        //me salgo por la derecha

        if (gridPosition.x > w)
        {
            gridPosition.x = -w;
        }
        //me salgo por la izquierda
        if (gridPosition.x < -w)
        {
            gridPosition.x = w;
        }
        //me salgo por la arriba
        if (gridPosition.y > h)
        {
            gridPosition.y = -h;
        }
        //me salgo por la abajo
        if (gridPosition.y > -h)
        {
            gridPosition.y = h;
        }

        return gridPosition;
    }

    private int Half(int number)   //me divide
    {
        return number /2;
    }
}
