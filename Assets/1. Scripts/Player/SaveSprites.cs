using System.IO;
using UnityEngine;

public class SaveSprites : MonoBehaviour
{
    public Sprite[] sprites; // 스프라이트를 저장할 배열
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
        // 원본 텍스처의 해상도를 유지합니다.
        Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);

        // 텍스처 필터링 모드를 Point로 설정하여 화질을 최대로 합니다.
        tex.filterMode = FilterMode.Point;
        // 텍스처 랩 모드를 Clamp로 설정하여 텍스처가 반복되지 않게 합니다.
        tex.wrapMode = TextureWrapMode.Clamp;

        // 텍스처의 읽기/쓰기 가능 여부를 확인하고, 픽셀을 가져옵니다.
        if (sprite.texture.isReadable)
        {
            Color[] pixels = sprite.texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height);
            tex.SetPixels(pixels);
            tex.Apply();
        }
        else
        {
            Debug.LogError("스프라이트 텍스처가 읽기 전용이 아닙니다. 'Read/Write Enabled'를 확인해 주세요.");
            return;
        }

        // 지정된 경로에 폴더가 없으면 생성
        CheckAndCreateDirectory(directoryPath);

        // PNG 형식으로 인코딩하여 바이트 배열을 얻습니다.
        byte[] bytes = tex.EncodeToPNG();
        string filePath = Path.Combine(directoryPath, filename + ".png");

        // 파일 시스템에 이미지 파일을 씁니다.
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
