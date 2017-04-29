using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseWell : MonoBehaviour
{
    private readonly List<GameObject> fallingParts = new List<GameObject>();
    public float HorizontalSpeed = 10f;
    public float RotationSpeed = 10f;
    public bool Enabled = true;

    public void Awake()
    {
        Enabled = true;
    }

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
        if (Enabled)
            ApplyControls();
        Apply(MovePart);
    }

    private void ApplyControls()
    {
        var horizontal = Input.GetAxis("Horizontal");
        if (Math.Abs(horizontal) > float.Epsilon)
            Apply(go => go.transform.Translate(Vector3.right * horizontal * HorizontalSpeed * Time.deltaTime, transform.parent));
        var rotation = Input.GetAxis("Rotation");
        if (Math.Abs(rotation) > float.Epsilon)
            Apply(go => go.transform.Rotate(Vector3.back * rotation * RotationSpeed * Time.deltaTime));
    }

    private void MovePart(GameObject go)
    {
        var position = (Vector2) go.transform.position;
        var translation = new Vector2(
                position.x - 0.5f * Time.deltaTime,
                position.y - 1 * Time.deltaTime
            );
        if (!Enabled)
            translation.x = position.x;
        go.GetComponent<Rigidbody2D>().MovePosition(translation);
    }

    private void Apply(Action<GameObject> action)
    {
        foreach (var part in fallingParts)
        {
            action(part);
        }
    }
}