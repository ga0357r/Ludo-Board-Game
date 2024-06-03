using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Data", menuName = "Ludo/Spawn Data", order = 1)]
public class SpawnData : ScriptableObject
{
    [SerializeField] private GameObject parent;
    [SerializeField] private List<GameObject> children;

    public GameObject Parent => parent;
    public List<GameObject> Children => children;
}
