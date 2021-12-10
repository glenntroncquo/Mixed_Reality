using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using UnityEngine.UI;


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

    
    public async void Order()
    {
        var responseString = await client.GetStringAsync("https://glenn.pagekite.me/pizza");
        txt.text = responseString;


        Debug.Log(ImageTracker.activePizza);
        
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
}
