public Camera arcamera;

#get the screen shoot of camera,and send it to server.
string getImage()
{
    RenderTexture rt =new RenderTexture(Screen.width,Screen.height,-1);
    arcamera.targetTexture=rt;
    // render the texture, otherwise, the texture is null.
    arcamera.Render();
    RenderTexture.active = rt;
    Texture2D photo = new Texture2D(Screen.width,Screen.height,textureFormat.RGB24,false);
    Rect rect = new Rect(0, 0, Screen.width, Screen.height);
    photo.ReadPixels(rect, 0, 0);
    photo.Apply();
    arcamera.targetTexture = null;
    RenderTexture.active = null;
    GameObject.Destroy(rt);
    byte[] bytes =photo.EncoderToPNG();
    return System.Convert.ToBase64String(bytes);
}

#resize Texture 2D
private Texture2D ScaleTexture(Texture2D raw, int W, int H)
    {
        Texture2D result = new Texture2D(W, H, raw.format, true);
        Color[] color = result.GetPixels(0);
        float incX = (1.0f / (float)W);
        float incY = (1.0f / (float)H);
        for (int i = 0; i< color.Length; i++)
        {
            color[i] = source.GetPixelBilinear(incX * ((float)i % W), incY * ((float)Mathf.Floor(i / W)));
        }
        result.SetPixels(color, 0);
        result.Apply();
        return result;
    }
