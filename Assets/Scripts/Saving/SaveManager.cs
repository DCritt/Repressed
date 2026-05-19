using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private string _picturePath;

    private int _pictureCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        _picturePath = Path.Combine(Application.persistentDataPath, "Savedata", "Photos");
        _pictureCount = Directory.GetFiles(_picturePath).Length;
        DeletePictures();
    }

    private string GetPictureName(int index)
    {
        return ("picture" + index + ".png");
    }

    public void DeletePictures()
    {
        foreach (string file in Directory.GetFiles(_picturePath))
        {
            File.Delete(file);
        }
        _pictureCount = 0;
    }

    public void SavePicture(Texture2D picture)
    {
        if (!Directory.Exists(_picturePath))
        {
            Directory.CreateDirectory(_picturePath);
        }

        byte[] data = picture.EncodeToPNG();

        string path = Path.Combine(_picturePath, GetPictureName(_pictureCount));
        File.WriteAllBytes(path, data);
        _pictureCount++;
    }

    public List<Texture2D> LoadPictures()
    {
        List<Texture2D> pictures = new List<Texture2D>();

        foreach (string file in Directory.GetFiles(_picturePath, "*.png"))
        {
            byte[] data = File.ReadAllBytes(file);

            Texture2D picture = new Texture2D(2, 2);
            picture.LoadImage(data);

            pictures.Add(picture);
        }

        return pictures;
    }

    public List<Texture2D> LoadSlicePictures(int index)
    {
        List<Texture2D> pictures = new List<Texture2D>();

        for (int i = index; i < _pictureCount; i++)
        {
            string path = Path.Combine(_picturePath, GetPictureName(i));
            byte[] data = File.ReadAllBytes(path);

            Texture2D picture = new Texture2D(2, 2);
            picture.LoadImage(data);

            pictures.Add(picture);
        }

        return pictures;
    }

    public Texture2D LoadPicture(int index)
    {
        Texture2D picture = new Texture2D(2, 2);

        string path = Path.Combine(_picturePath, GetPictureName(index));
        byte[] data = File.ReadAllBytes(path);

        picture.LoadImage(data);

        return picture;
    }
}