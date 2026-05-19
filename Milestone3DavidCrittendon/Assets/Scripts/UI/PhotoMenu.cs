using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoMenu : MonoBehaviour
{
    [SerializeField] private PaginatedItemGroup _pictureHolder;
    [SerializeField] private GameObject _picturePrefab;

    private int _pictureIndex = 0;

    private void Start()
    {
    
    }

    private void OnEnable()
    {
        LoadNewPhotos();
    }

    private void LoadNewPhotos()
    {
        List<Texture2D> pictureTextures = PictureCacheManager.Instance.GetSlicePictureTextures(_pictureIndex);

        foreach (Texture2D texture in pictureTextures)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            GameObject picture = Instantiate(_picturePrefab);
            Image image = picture.GetComponent<Image>();
            image.sprite = sprite;

            _pictureHolder.AddItem(picture);
            _pictureIndex++;
        }
    }
}
