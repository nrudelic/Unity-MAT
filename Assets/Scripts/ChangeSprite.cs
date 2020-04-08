using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    private SpriteRenderer rend;
    private Sprite symbol1, symbol2;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        symbol1 = Resources.Load<Sprite>("/Symbols/symbol1");
        rend.sprite = symbol1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int nextInt = Random.Range(0, 21) + 1;
            symbol1 = Resources.Load<Sprite>("Symbols/symbol" + nextInt.ToString());
            rend.sprite = symbol1;
            print("1");
        }
    }
}
