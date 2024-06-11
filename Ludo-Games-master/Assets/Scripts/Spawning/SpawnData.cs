using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Data", menuName = "Ludo/Spawn Data", order = 1)]
public class SpawnData : ScriptableObject
{
    [System.Serializable]
    public class ParentWithChildren
    {
        public string name;
        public GameObject parent;
        public List<GameObject> children = new List<GameObject>();
    }

    [SerializeField] private List<ParentWithChildren> parentsWithChildren = new List<ParentWithChildren>();

    public List<ParentWithChildren> ParentsWithChildren => parentsWithChildren;
}
