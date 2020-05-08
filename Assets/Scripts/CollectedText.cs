using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectedText : MonoBehaviour
{
    public int AddedLikes = 1;

    private float _duration = 1f;
    private Color _currColor;

    private void Awake()
    {
        SetText(AddedLikes);
    }
    void Start()
    {
        _currColor = GetComponent<Text>().color;
        Destroy(gameObject, _duration);
    }

    // Update is called once per frame
    void Update()
    {
        //Move up
        transform.Translate(Vector3.up * 50 * _duration * Time.deltaTime);
        //Fade Out
        GetComponent<Text>().color = new Color(_currColor.r, _currColor.g, _currColor.b, _currColor.a - (Time.deltaTime / _duration));
        _currColor = GetComponent<Text>().color;
    }
    public void SetText(int addedLikes)
    {
        gameObject.GetComponent<Text>().text = "+" + addedLikes;
    }
}
