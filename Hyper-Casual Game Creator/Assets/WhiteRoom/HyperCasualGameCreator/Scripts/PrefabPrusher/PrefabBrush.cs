using System.Collections.Generic;
using UnityEngine;

public class PrefabBrush : Manager
{
    public Transform parent = null;

    public Vector3 generalOffset;

    public GameObject previewCirle;

    public List<Brush> brushes = new List<Brush>();

    [HideInInspector] public List<GameObject> brushedPrefab;
}

public class Brush
{
    public bool isSelected;
    
    public Object prefab;
    
    public float size;
    public float rotation;
    
    public Vector3 localOffset;

    public Brush(Object _prefab, float _size, float _rotation, Vector3 _localOffset)
    {
        isSelected = false;
        
        prefab = _prefab;
        size = _size;
        rotation = _rotation;
        localOffset = _localOffset;
    }
}
