using UnityEngine;

public class BuildingDefenses : MonoBehaviour
{
    public static BuildingDefenses instance;

    public bool isBuilding;
    public GameObject buildMenuCanvas;

    public GameObject objectToBuild;
    public GameObject objectHologram;

    public KeyCode buildButton = KeyCode.B;
    public KeyCode placeButton = KeyCode.E;

    private ContactFilter2D hollogramCollisionFilter;
    private readonly Collider2D[] hollogramCollisionResults = new Collider2D[10];
    private GameObject objectHologramPrefab;

    private GameObject objectToBuildPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        hollogramCollisionFilter.useTriggers = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(buildButton))
        {
            isBuilding = !isBuilding;
            buildMenuCanvas.SetActive(!buildMenuCanvas.activeInHierarchy);
            if (isBuilding && objectHologramPrefab != null)
                CreateHologram();
            else
                DeleteHologram();
        }

        if (!isBuilding) return;

        if (objectHologram != null)
            objectHologram.transform.position = MouseWorldPosition();

        if (Input.GetKeyDown(placeButton) && !HologramCollidingWithColliders())
            SpawnObjectAtLocation(objectToBuildPrefab, objectHologram.transform.position);
    }

    public void SetDefense(GameObject objectToBuildPrefab, GameObject objectHologramPrefab)
    {
        this.objectToBuildPrefab = objectToBuildPrefab;
        this.objectHologramPrefab = objectHologramPrefab;
        DeleteHologram();
        CreateHologram();
    }

    private void CreateHologram()
    {
        objectHologram = Instantiate(objectHologramPrefab);
    }

    private void DeleteHologram()
    {
        Destroy(objectHologram);
    }

    private Vector2 MouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool HologramCollidingWithColliders()
    {
        return objectHologram.GetComponent<Collider2D>()
            .OverlapCollider(hollogramCollisionFilter, hollogramCollisionResults) > 0;
    }

    private void SpawnObjectAtLocation(GameObject obj, Vector3 position)
    {
        Instantiate(obj, position, Quaternion.identity);
    }

    public void StopBuilding()
    {
        buildMenuCanvas.SetActive(false);
        DeleteHologram();
    }
}