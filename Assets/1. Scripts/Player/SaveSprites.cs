using System.IO;
using UnityEngine;

public class SaveSprites : MonoBehaviour
{
    public Sprite[] sprites; // ��������Ʈ�� ������ �迭
    private string directoryPath = @"C:\Users\wjj32\AppData\Roaming\SaveImage";

    void Start()
    {
        int index = 0;
        foreach (Sprite sprite in sprites)
        {
            SaveSpriteToFile(sprite, "sprite_" + index);
            index++;
        }
    }

    void SaveSpriteToFile(Sprite sprite, string filename)
    {
        // ���� �ؽ�ó�� �ػ󵵸� �����մϴ�.
        Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);

        // �ؽ�ó ���͸� ��带 Point�� �����Ͽ� ȭ���� �ִ�� �մϴ�.
        tex.filterMode = FilterMode.Point;
        // �ؽ�ó �� ��带 Clamp�� �����Ͽ� �ؽ�ó�� �ݺ����� �ʰ� �մϴ�.
        tex.wrapMode = TextureWrapMode.Clamp;

        // �ؽ�ó�� �б�/���� ���� ���θ� Ȯ���ϰ�, �ȼ��� �����ɴϴ�.
        if (sprite.texture.isReadable)
        {
            Color[] pixels = sprite.texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height);
            tex.SetPixels(pixels);
            tex.Apply();
        }
        else
        {
            Debug.LogError("��������Ʈ �ؽ�ó�� �б� ������ �ƴմϴ�. 'Read/Write Enabled'�� Ȯ���� �ּ���.");
            return;
        }

        // ������ ��ο� ������ ������ ����
        CheckAndCreateDirectory(directoryPath);

        // PNG �������� ���ڵ��Ͽ� ����Ʈ �迭�� ����ϴ�.
        byte[] bytes = tex.EncodeToPNG();
        string filePath = Path.Combine(directoryPath, filename + ".png");

        // ���� �ý��ۿ� �̹��� ������ ���ϴ�.
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Saved Sprite as PNG: " + filePath);
    }


    void CheckAndCreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
