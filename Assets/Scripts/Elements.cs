using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tetris Elements", menuName = "Elements")]
public class Elements : ScriptableObject
{
    public string prefabName;

    public int points;

    public GameObject prefab;

    public Sprite sprite;

    public Vector3[] spawnPoints;




}
