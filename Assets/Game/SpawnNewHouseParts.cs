using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnNewHouseParts : MonoBehaviour
{
    public Transform spawnPosition;
    public List<GameObject> Prefabs;
    public float SpawnChance = 0.1f;
    public float MaximumQuietDuration = 2.5f;
    public float MinimumQuietDuration = 1f;
    private float sinceLastSpawn = 0f;

    private void Awake()
    {
        if (!Prefabs.Any())
            throw new Exception("Add some stuff to spawner");
    }

    private void Start()
    {
        //SpawnOnRandomSide();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;
        if (other.CompareTag("Zone"))
        {
            Prefabs = other.gameObject.GetComponent<ZoneSwitcher>().ZonePrefabs;
            Restarter.instance.Save(transform.position);
        }
    }

    private void Update()
    {
        sinceLastSpawn += Time.deltaTime;
        if (sinceLastSpawn < MaximumQuietDuration) return;
        var shouldSpawn = sinceLastSpawn > MaximumQuietDuration || CheckChance();
        if (!shouldSpawn) return;
        sinceLastSpawn = 0;
        SpawnOnRandomSide();
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

    private void SpawnOnRandomSide()
    {
        var prefab = GetRandomPrefab();
        // Disable random, lol
        //if (Random.value > 0.5f)
            SpawnFromLeft(prefab);
        //else
        //    SpawnFromRight(prefab);
    }

    private void SpawnFromLeft(GameObject prefab)
    {
        Spawn(prefab, spawnPosition.transform.position + Vector3.left * 10, Vector2.right);
    }

    private void SpawnFromRight(GameObject prefab)
    {
        Spawn(prefab, spawnPosition.transform.position + Vector3.right * 10, Vector2.left);
    }

    private void Spawn(GameObject prefab, Vector2 start, Vector2 direction)
    {
        var go = Instantiate(prefab);
        go.transform.SetParent(spawnPosition.transform.parent);
        go.transform.position = (Vector3)start + Vector3.back * 5;
        var move = go.AddComponent<MoveInDirection>();
        move.Direction = direction;
        move.Speed = 1;
        go.SetLayerRecoursively("Floating");
        var rb = go.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        go.AddComponent<DestroyAfterTime>();
        if (Camera.main.transform.WorldPosition().y > 4)
            go.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        go.AddComponent<Oscillate>();
    }
}