using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToppingHandler : MonoBehaviour
{
    public Text ToppingName;
    public Sprite Bacon;
    public Sprite Cheese;
    public Sprite Chili;
    public Sprite Ham;
    public Sprite Peperoni;
    public static string activeTopping = "Ham";
    public static Sprite activeToppingSprite;
    public Dictionary<string, Sprite> sprites;
    public Sprite img;
    public RectTransform rt;

    public int positionX = 180;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void toppingCLick()
    {
        // set title
        activeTopping = EventSystem.current.currentSelectedGameObject.name;
        ToppingName.text = activeTopping;

        var sprites = new Dictionary<string, Sprite>()
        {
            ["Bacon"] = Bacon,
            ["Cheese"] = Cheese,
            ["Chili"] = Chili,
            ["Ham"] = Ham,
            ["Peperoni"] = Peperoni,
        };
        img = sprites[activeTopping];

        GameObject go = new GameObject();
        Image image = go.gameObject.AddComponent<Image>();
        image.sprite = img;
        go.transform.SetParent(rt, true);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector3(positionX, 0, 0);
        go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        positionX = positionX - 100;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
