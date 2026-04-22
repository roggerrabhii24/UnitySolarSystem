using UnityEngine;
using System.Collections;

public class sun_luminance : MonoBehaviour
{
    // Array of Objects that stores the results of Resources.LoadAll()
    private Object[] objects;
    // Each returned object is converted to a Texture and stored here
    private Texture[] textures;
    // Cached reference to this object's Renderer
    private Renderer goRenderer;

    private float speed = 10f;
    private float time = 0f;
    private int prev_time = 0;

    void Awake()
    {
        // Cache the Renderer component (this.renderer was removed in Unity 5)
        goRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        // Load all textures from the luma folder inside Resources
        this.objects = Resources.LoadAll("luma", typeof(Texture));

        this.textures = new Texture[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            this.textures[i] = (Texture)this.objects[i];
        }
    }

    void Update()
    {
        if (goRenderer == null) return;

        time += Time.deltaTime * speed;
        if (prev_time != Mathf.RoundToInt(time))
        {
            prev_time = Mathf.RoundToInt(time);
            if (prev_time >= objects.Length)
            {
                prev_time = 0;
                time = 0f;
            }
            goRenderer.material.SetTexture("_Illum", this.textures[prev_time]);
        }
    }
}
