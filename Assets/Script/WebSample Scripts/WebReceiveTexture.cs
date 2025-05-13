using UnityEngine;

public class WebSample : MonoBehaviour
{
    public Renderer targetRenderer; // 3D 오브젝트의 Renderer
    public string shaderTextureProperty = "_Img"; // 쉐이더 텍스처 변수명
    public string localTextureName = "testTex"; // Resources 폴더 내 텍스처 이름(확장자 제외)


    private void Start()
    {
        // LoadLocalTexture();
    }

    public void SendGetFile()
    {
        Debug.Log("SendGetFile");
        StartCoroutine(Api_GetFile.Send(
            Api_UploadFile.LatestUploadTextureFilename,
            sprite =>
            {
                if (targetRenderer != null && sprite != null)
                {
                    Texture2D tex = sprite.texture;
                    targetRenderer.material.SetTexture(shaderTextureProperty, tex);
                }
            }));
    }

    // 서버 대신 로컬 이미지로 테스트
    public void LoadLocalTexture()
    {
        Texture2D tex = Resources.Load<Texture2D>(localTextureName);
        if (tex != null && targetRenderer != null)
        {
            targetRenderer.material.SetTexture(shaderTextureProperty, tex);
            Debug.Log("로컬 텍스처 적용 완료!");
        }
        else
        {
            Debug.LogWarning("텍스처 또는 렌더러를 찾을 수 없습니다.");
        }
    }

}
