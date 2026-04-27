using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotoMenu : MonoBehaviour
{
    [SerializeField] private PaginatedItemGroup _pictureHolder;
    [SerializeField] private GameObject _picturePrefab;
    [SerializeField] private TextMeshProUGUI _currPageText;
    [SerializeField] private GameObject _fullscreenPhoto;

    private int _pictureIndex = 0;

    private void Start()
    {
    
    }

    private void OnEnable()
    {
        CloseFullscreenImage();
        LoadNewPhotos();
        UpdatePageIndicator();
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

            Button button = picture.GetComponent<Button>();
            button.onClick.AddListener(() => OpenFullscreenImage(sprite));

            TextMeshProUGUI photoNumber = picture.GetComponentInChildren<TextMeshProUGUI>();
            photoNumber.text = _pictureIndex.ToString();

            _pictureHolder.AddItem(picture);
            _pictureIndex++;
        }
    }

    public void OpenFullscreenImage(Sprite sprite)
    {
        Image image = _fullscreenPhoto.GetComponent<Image>();
        image.sprite = sprite;
        _fullscreenPhoto.SetActive(true);
    }

    public void CloseFullscreenImage()
    {
        _fullscreenPhoto.SetActive(false);
    }

    public void UpdatePageIndicator()
    {
        _currPageText.text = _pictureHolder.CurrPage + "/" + _pictureHolder.MaxPage;
    }
}
