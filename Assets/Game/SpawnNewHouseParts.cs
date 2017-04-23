using UnityEngine;

public class SpawnNewHouseParts : MonoBehaviour
{
    public GameObject[] p;

    private void Start()
    {
        p = Resources.LoadAll<GameObject>("Prefabs/HouseObjects");
    }

    private void Update()
    {

    }
}