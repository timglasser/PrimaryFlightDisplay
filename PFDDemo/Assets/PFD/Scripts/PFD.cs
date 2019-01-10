
//To the extent possible under law, 
//Tim Glasser 
//tim_glasser@hotmail.com
//has waived all copyright and related or neighboring rights to
//Unity Primary Flight Display (FPV) C# Classes.
//This work is published from:
//California.

// As indicated by the Creative Commons, the text on this page may be copied, 
// modified and adapted for your use, without any other permission from the author.

// Please do not remove this notice

using UnityEngine;
using UnityEngine.UI;

public class PFD : MonoBehaviour
{

    // sensitivity
    // start point
    public RawImage boreSight;
    public RawImage fpv;
    public RawImage pitchLadder;
    public RawImage heading;
    public RawImage roll;
    public RawImage alt;
    public RawImage airSpeed;
    public Rigidbody player;
    public Material mat;
    public float fpvSensitivity = 300.0f;

    private Text _alttxt;
    private Text _speedtxt;

    private int _offset;
    private int _w;
    private int _h;

    void Start()
    {
        // the text is a child of the containing box image
        _alttxt = alt.transform.GetComponentInChildren<Text>();
        _speedtxt = airSpeed.transform.GetComponentInChildren<Text>();
    }

    void LateUpdate()
    {
        
        // calc fp vector from world rb velocity
        Vector3 vel = player.velocity;

        // calc speed
        float speed = vel.magnitude;

        // set value
        _speedtxt.text = speed.ToString("n0"); // 0 dp Number
        _alttxt.text = player.transform.position.y.ToString("n0"); // 0 dp Number
     
        float _yangle = Vector3.Angle(vel, transform.right) - 90.0f;
        float _xangle = Vector3.Angle(vel, transform.up) - 90.0f;

        float ydist = Mathf.Tan(_xangle * (Mathf.PI/180.0f)) * fpvSensitivity;  // to rads  
        float xdist = Mathf.Tan(_yangle * (Mathf.PI / 180.0f)) * fpvSensitivity;  // to rads

        fpv.transform.localPosition = new Vector3(xdist, ydist, 0.0f);

        // z angle
      //  float zangle = Vector3.Angle(vel, transform.up) - 90.0f;

        pitchLadder.uvRect = new Rect(0.0f, (-player.transform.eulerAngles.x / 90.0f) + 0.5f, 1f, 0.2f);
        pitchLadder.rectTransform.localEulerAngles= new Vector3(0.0f, 0.0f, -player.transform.eulerAngles.z);

        heading.uvRect = new Rect(player.transform.eulerAngles.y / 360f, 0, 1f, 1f);
   
        alt.uvRect= new Rect(0.0f, player.transform.position.y /200.0f , 1f, 0.2f);
        airSpeed.uvRect = new Rect(0.0f, speed / 200.0f, 1f, 0.2f);

    }

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    // Draw red a rombus on the screen
    // and also draw a small cyan Quad in the left corner

    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        GL.Color(Color.red);
        GL.Vertex3(20, 30, 0);
        GL.Vertex3(30f, 40, 0);
        GL.Vertex3(40, 30f, 0);
        GL.Vertex3(30f, 20, 0);

        GL.Color(Color.cyan);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, 0.25f, 0);
        GL.Vertex3(0.25f, 0.25f, 0);
        GL.Vertex3(0.25f, 0, 0);
        GL.End();
        GL.PopMatrix();
    }
   

    static public Texture2D GetRTPixels(RenderTexture rt)
    {
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = rt;

        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        // Restore previously active render texture
        RenderTexture.active = currentActiveRT;
        return tex;
    }
}

