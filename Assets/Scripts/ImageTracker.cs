using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefabs;
    private Button[] buttons;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    public static string activePizza = "Pepperoni";
    public Text pizzaName;
    public Camera camera;
    private bool rotate = false;
    private GameObject prefab;

    private void Awake()
    {
        pizzaName.text = activePizza;
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        foreach(GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.SetActive(false);
            
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }

    }
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;

    }

    private void OnDisable(){
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[activePizza].SetActive(false);
        }
        
        
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        Vector3 position = trackedImage.transform.position;

        prefab = spawnedPrefabs[activePizza];
        prefab.transform.position = position;
        prefab.SetActive(true);

        foreach(GameObject go in spawnedPrefabs.Values)
        {
            if(go.name != activePizza){
                go.SetActive(false);
            }
        }
    }

    public void HandlePizza()
    {
        activePizza = EventSystem.current.currentSelectedGameObject.name;
        pizzaName.text = activePizza;
    }

    public void HandleNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if(rotate == true){
            prefab.transform.Rotate(new Vector3(100f, 100f, 0f) * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.transform.name == activePizza)
                {
                    rotate = !rotate;
                }
                
            }
        }
    }
}
