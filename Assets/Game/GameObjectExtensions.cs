using UnityEngine;

public static class GameObjectExtensions
{
    public static void SetCollidersEnabled(this GameObject gameObject, bool value)
    {
        foreach (var component in gameObject.GetComponents<Collider2D>())
        {
            component.enabled = value;
        }
    }

    public static void DisableAllBehaviors(this GameObject gameObject)
    {
        foreach (var component in gameObject.GetComponents<MonoBehaviour>())
        {
            component.enabled = false;
        }
    }

    public static void SetLayerRecoursively(this GameObject gameObject, string layer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
        foreach (Transform transform in gameObject.transform)
        {
            transform.gameObject.SetLayerRecoursively(layer);
        }
    }

    public static Vector3 WorldPosition(this GameObject gameObject)
    {
        return gameObject.transform.WorldPosition();
    }

    public static Vector3 WorldPosition(this Transform transform)
    {
        return transform.TransformPoint(Vector3.zero);
    }
}