using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OrderHandler : MonoBehaviour
{
    public Text txt;
    private static readonly HttpClient client = new HttpClient();
    public Sprite Pepperoni;
    public Sprite American;
    public Sprite Ham;
    public Sprite Forestiere;
    public Sprite Cheese;
    public Sprite Hawai;
    public Image img;
    [SerializeField] RectTransform fader;
    public Image loader;
    public Text text;
    public static string responseString;


    public async void Order()
    {
        var responseString = await client.GetStringAsync("https://glenn.pagekite.me/pizza");
        txt.text = responseString;
        
    }

    private void Start()
    {
        var sprites = new Dictionary<string, Sprite>()
        {
            ["Pepperoni"] = Pepperoni,
            ["American"] = American,
            ["Ham"] = Ham,
            ["Forestiere"] = Forestiere,
            ["Cheese"] = Cheese,
            ["Hawai"] = Hawai,
        };

        img.sprite = sprites[ImageTracker.activePizza];
    }

    public void HandleLoader()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.4f).setOnComplete(async () =>
        {
            loader.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            try{
                responseString = await client.GetStringAsync("https://glenn.pagekite.me/pizza");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }catch{
                Debug.Log("An error occured");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        });
    }
    void Update () 
    {
        loader.transform.Rotate(Vector3.forward * Time.deltaTime * 100f);
    }
}
