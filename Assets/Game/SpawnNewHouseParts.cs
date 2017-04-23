using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnNewHouseParts : MonoBehaviour
{
    public List<GameObject> Prefabs;
    public float SpawnChance = 0.1f;

    private void Awake()
    {
        if (!Prefabs.Any())
            throw new Exception("Add some stuff to spawner");
    }

    private void Update()
    {
        if (!CheckChance()) return;
        var prefab = GetRandomPrefab();
        if (Random.value > 0.5f)
            SpawnFromLeft(prefab);
        else
            SpawnFromRight(prefab);
    }

    private bool CheckChance()
    {
        var random = Random.value;
        return random < SpawnChance;
    }

    private GameObject GetRandomPrefab()
    {
        return Prefabs[Random.Range(0, Prefabs.Count)];
    }

    private void SpawnFromLeft(GameObject prefab)
    {
        Spawn(prefab, transform.position + Vector3.left * 10, Vector2.right);
    }

    private void SpawnFromRight(GameObject prefab)
    {
        Spawn(prefab, transform.position + Vector3.right * 10, Vector2.left);
    }

    private void Spawn(GameObject prefab, Vector2 start, Vector2 direction)
    {
        var go = Instantiate(prefab);
        go.transform.SetParent(transform.parent);
        go.transform.position = (Vector3)start + Vector3.back * 5;
        var move = go.AddComponent<MoveInDirection>();
        move.Direction = direction;
        move.Speed = 1;
        go.layer = LayerMask.NameToLayer("Floating");
    }
}

public class MoveInDirection : MonoBehaviour
{
    public float Speed;
    public Vector2 Direction;

    private void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
    }
}