using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using Newtonsoft.Json;

public class OrderHandler : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();
    public Sprite Pepperoni;
    public Sprite Margherita;
    public Sprite HamPizza;
    public Sprite Forestiere;
    public Sprite Barbecue;
    public Sprite Hawai;
    public  Sprite Bacon;
    public  Sprite Cheese;
    public  Sprite Chili;
    public  Sprite Ham;
    public  Sprite Peperoni;
    public Image img;
    [SerializeField] RectTransform fader;
    public Text activePizzaName;
    public RectTransform rt;
    public int positionX = -291;

    private void Start()
    {
        var sprites = new Dictionary<string, Sprite>()
        {
            ["Pepperoni"] = Peperoni,
            ["Margherita"] = Margherita,
            ["Ham"] = HamPizza,
            ["Forestiere"] = Forestiere,
            ["Barbecue"] = Barbecue,
            ["Hawai"] = Hawai,
        };

        img.sprite = sprites[ImageTracker.activePizza];
        activePizzaName.text ="\n  "+ ImageTracker.activePizza;

        var spriteToppings= new Dictionary<string, Sprite>()
        {
            ["Bacon"] = Bacon,
            ["Cheese"] = Cheese,
            ["Chili"] = Chili,
            ["Ham"] = Ham,
            ["Peperoni"] = Pepperoni,
        };



        foreach(var topping in ToppingHandler.toppingsDict)
        {
            if(spriteToppings.ContainsKey(topping.Key))
            {
                GameObject go = new GameObject();
                Image image = go.gameObject.AddComponent<Image>();
                image.sprite = spriteToppings[topping.Key];


                go.transform.SetParent(rt, true);
                go.GetComponent<RectTransform>().anchoredPosition = new Vector3(positionX, 0, 0);
                go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            positionX += 150;
        }
    }

    public void HandleLoader()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.4f).setOnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }
}
