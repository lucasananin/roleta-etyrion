using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadImageToUIImage : MonoBehaviour
{
    [SerializeField] Image targetImage;
    [SerializeField] string _fileName = null;

    [Header("Options")]
    public bool preserveAspect = true;
    //public bool setNativeSize = true;

    private void Start()
    {
        LoadImage();
    }

    //private IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(1f);
    //    LoadImage();
    //}

    public void LoadImage()
    {
        string path = GetFilePath();

        if (string.IsNullOrEmpty(path))
        {
            Debug.Log("No file selected.");
            return;
        }

        if (!File.Exists(path))
        {
            Debug.LogError("File not found: " + path);
            return;
        }

        Texture2D texture = LoadTexture(path);

        if (texture == null)
        {
            Debug.LogError("Failed to load image.");
            return;
        }

        ApplyTexture(texture);
    }

    Texture2D LoadTexture(string path)
    {
        try
        {
            byte[] fileData = File.ReadAllBytes(path);

            Texture2D texture = new(
                2,
                2,
                TextureFormat.RGBA32,
                false
            );

            if (!texture.LoadImage(fileData))
            {
                Debug.LogError("Unsupported or corrupted image.");
                return null;
            }

            texture.name = Path.GetFileName(path);

            return texture;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading image: " + e.Message);
            return null;
        }
    }

    void ApplyTexture(Texture2D texture)
    {
        if (targetImage == null)
        {
            Debug.LogError("Target Image not assigned.");
            return;
        }

        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );

        targetImage.sprite = sprite;
        targetImage.color = Color.white;

        if (preserveAspect)
            targetImage.preserveAspect = true;

        //if (setNativeSize)
        //    targetImage.SetNativeSize();
    }

    string GetFilePath()
    {
#if UNITY_EDITOR
        //return EditorUtility.OpenFilePanel(
        //    "Select Image",
        //    "",
        //    "png,jpg,jpeg,bmp,tga,gif"
        //);
        string projectRoot = Directory.GetParent(Application.dataPath).FullName;
        return Path.Combine(projectRoot, $"{_fileName}.png");
#else
        return Path.Combine(Application.dataPath, "..", $"{_fileName}.png");
        //Debug.LogWarning("Runtime file picker not implemented. Use a file browser plugin for builds.");
        //return null;
#endif
    }
}