using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseWell : MonoBehaviour
{
    private readonly List<GameObject> fallingParts = new List<GameObject>();
    public float HorizontalSpeed = 10f;
    public float RotationSpeed = 10f;

    public void Add(GameObject part)
    {
        fallingParts.Add(part);
    }

    public void Remove(GameObject part)
    {
        fallingParts.Remove(part);
    }

    private void Update()
    {
        if (!fallingParts.Any()) return;
        var horizontal = Input.GetAxis("Horizontal");
        if (Math.Abs(horizontal) > float.Epsilon)
            Apply(go => go.transform.Translate(Vector3.right * horizontal * HorizontalSpeed * Time.deltaTime, transform.parent));
        var rotation = Input.GetAxis("Rotation");
        if (Math.Abs(rotation) > float.Epsilon)
            Apply(go => go.transform.Rotate(Vector3.back * rotation * RotationSpeed * Time.deltaTime));
    }

    private void Apply(Action<GameObject> action)
    {
        foreach (var part in fallingParts)
        {
            action(part);
        }
    }
}