using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 涟漪效果  绘制原理 当前帧的上下左右四个像素值的和除2再减去上一帧像素的值
/// </summary>

public class Ripple : MonoBehaviour
{

    public Camera mainCamera;
    /// <summary>
    /// 绘制涟漪效果的纹理图对象 -- 上一帧
    /// </summary>
    public RenderTexture LastRT;
    /// <summary>
    /// 绘制涟漪效果的纹理图对象 -- 当前帧
    /// </summary>
    public RenderTexture CurrentRT;
    public RenderTexture TempRT;
    /// <summary>
    /// 绘制涟漪
    /// </summary>
    public Shader DrawShader;
    [SerializeField][Range(0,1.0f)]
    private float DrawRadius = 0.2f;
    private Material DrawMat;

    /// <summary>
    /// 计算涟漪高度
    /// </summary>
    public Shader RippleShader;
    private Material RippleMat;

    /// <summary>
    /// 绘制效果的大小定义
    /// </summary>
    public int TextureSize = 512;
    // Start is called before the first frame update
    void Start()
    {
        // 获取主相机
        mainCamera = Camera.main.GetComponent<Camera>();

        LastRT = CreateRT();
        CurrentRT = CreateRT();
        TempRT = CreateRT();

        DrawMat = new Material(DrawShader);
        RippleMat = new Material(RippleShader);

        GetComponent<Renderer>().material.mainTexture = CurrentRT;
    }

    /// <summary>
    /// 创建涟漪效果
    /// </summary>
    /// <returns></returns>
    public RenderTexture CreateRT()
    {
        RenderTexture rt = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.RFloat);
        rt.Create();
        return rt;
    }

    // Update is called once per frame
    void Update()
    {
        // 鼠标按下是射线检测
        if (Input.GetMouseButton(0))
        {
            // 由点击点创建射线
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 拿到射线的碰撞信息，然后绘制图案
                DrawRipple(hit.textureCoord.x, hit.textureCoord.y, DrawRadius);
            }

            // 计算涟漪
            RippleMat.SetTexture("_LastRT", LastRT);
            RippleMat.SetTexture("_CurrentRT", CurrentRT);
            Graphics.Blit(null, TempRT, RippleMat);
            // 将计算值交换到当前帧上
            Graphics.Blit(TempRT, LastRT);
            // 更新当前帧指向shader
            (CurrentRT, LastRT) = (LastRT, CurrentRT);
        }
    }

    /// <summary>
    /// 绘制涟漪图案
    /// </summary>
    /// <param name=""></param>
    /// <param name="y"></param>
    /// <param name="radius"></param>
    private void DrawRipple(float x,float y, float radius) {
        // 贴图
        DrawMat.SetTexture("_SourceTex", CurrentRT);
        // 绘制位置和大小信息
        DrawMat.SetVector("_Pos", new Vector4(x, y, radius));
        // 缓存绘制
        Graphics.Blit(null, TempRT,DrawMat);

        // 交换绘制
        (CurrentRT, TempRT) = (TempRT, CurrentRT);
    }
}
