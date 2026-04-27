using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PictureCacheManager : MonoBehaviour
{
    public static PictureCacheManager Instance { get; private set; }

    private List<Texture2D> _pictureTextures = new List<Texture2D>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public List<Texture2D> GetPictureTextures()
    {
        LoadNewTextures();
        return new List<Texture2D>(_pictureTextures);
    }

    public List<Texture2D> GetSlicePictureTextures(int index)
    {
        LoadNewTextures();

        List<Texture2D> pictures = new List<Texture2D>();
        for (int i = index; i < _pictureTextures.Count; i++)
        {
            pictures.Add(_pictureTextures[i]);
        }

        return pictures;
    }

    public Texture2D GetPictureTexture(int index)
    {
        LoadNewTextures();

        return _pictureTextures[index];
    }

    private void LoadNewTextures()
    {
        List<Texture2D> newPictures = SaveManager.Instance.LoadSlicePictures(_pictureTextures.Count);
        foreach (Texture2D texture in newPictures)
        {
            _pictureTextures.Add(texture);
        }
    }
}
