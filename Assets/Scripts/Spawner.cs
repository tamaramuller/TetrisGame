using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public Image elementImage;
    public Elements[] elements;
    public GameObject gameOver;

    public static Transform[,] grid = new Transform[11, 20];

    Elements willSpawnNext; // What will spawn next
    Elements currentTetronimo;
    GameObject currentElement; // Current tetronimo gameobject
    ScorePoints score;
  

    private float fall = 0f;
    private float fallSpeed = 1f;

    void Start()
    {
        score = GetComponent<ScorePoints>();
        Elements startElement = elements[Random.Range(0, elements.Length)]; // First tetronimo
        SpawnEntities(startElement);
        currentTetronimo = startElement;


    }

    void SpawnEntities(Elements element)
    {
        // Randomizing the next element
        willSpawnNext = elements[Random.Range(0, elements.Length)];
        elementImage.sprite = willSpawnNext.sprite;
        
        
        // Instantiate the tetronimo
        if (elements.Length > 0)
        {
            currentElement = willSpawnNext.prefab;
            currentTetronimo = willSpawnNext;
            int currentSpawnPointIndex = 0;
            currentElement = Instantiate(element.prefab, element.spawnPoints[currentSpawnPointIndex], Quaternion.identity);
        }

        
    }

    void Update()
    {
        // Userimput: LEFT ARROW
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentElement.transform.position += new Vector3(-1, 0, 0);

            if (!IsInBorder())
            {
                currentElement.transform.position += new Vector3(1, 0, 0);
            }

        }

        // Userimput: RIGHT ARROW
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentElement.transform.position += new Vector3(1, 0, 0);
            
            if (!IsInBorder())
            {
                currentElement.transform.position += new Vector3(-1, 0, 0);
            }
        }

        // Userimput: UP ARROW
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentElement.transform.Rotate(0, 0, -90);

            if (!IsInBorder())
            {
                currentElement.transform.Rotate(0, 0, 90);
            }
        }

        // Userimput: DOWN ARROW
        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            currentElement.transform.position += new Vector3(0, -1, 0);
            
            if (!IsInBorder())
            {
                currentElement.transform.position += new Vector3(0, 1, 0);
                Debug.Log(currentElement.transform.position);
                GridUpdate(currentElement); // Storing the element's coordinates 
                score.AddPoints(currentTetronimo);
                SpawnEntities(willSpawnNext); // Spawn the next element        
            }
            fall = Time.time;
        }
    }

    public bool IsInGrid(Vector2 position)
    {
        return ((int)position.x >= 0 && (int)position.x <= 10 && (int)position.y >= 0 && (int)position.y <= 20);
    }

    public bool IsInBorder()
    {
        foreach (Transform child in currentElement.transform)
        {
            Vector2 vector = child.position;
            if (!IsInGrid(vector))
            {
                return false;
            }
            int roundedX = Mathf.RoundToInt(child.position.x);
            int roundedY = Mathf.RoundToInt(child.position.y);
            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
            
        }
        return true;
        

    }

    public void GridUpdate(GameObject current)
    {
        foreach (Transform children in current.transform)
        {
            int roundedX = Mathf.RoundToInt(children.position.x);
            int roundedY = Mathf.RoundToInt(children.position.y);
            grid[roundedX, roundedY] = children;

            if(roundedY >= 18 ){
                Time.timeScale = 0;
                Debug.Log("Game over");
                gameOver.SetActive(true);
            }

        }

    }


}
