using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "ImageListData", menuName = "Project L/ImageListData")]
public class ImageListData : ScriptableObject
{
    public List<Image> Images;

    public Image GetRandom()
    {
        return Images[UnityEngine.Random.Range(0, Images.Count)];
    }
}
