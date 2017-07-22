using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateHeightMap : MonoBehaviour {
    public Terrain terrain;
    public Image heightMap;
    Texture2D _draw;

	// Use this for initialization
	void Start () {
        _draw = new Texture2D((int)terrain.terrainData.size.x, (int)terrain.terrainData.size.z, TextureFormat.ARGB32, false);

        int mipCount = Mathf.Min(3, _draw.mipmapCount);
        float[,] heights = terrain.terrainData.GetHeights(0,0,(int)terrain.terrainData.size.x, (int)terrain.terrainData.size.z);
        int i = 0;
        Color c;
        for (int mip = 0; mip < mipCount; ++mip)
        {
            Color[] cols = _draw.GetPixels(mip);
            i = 0;
            for (int k = 0; k < (int)terrain.terrainData.size.x; ++k)
            {
                for (int j = 0; j < (int)terrain.terrainData.size.z; ++j)
                {
                    c = Color.white * (heights[k, j]);
                    c.a = 1f;
                    cols[i++] = c;
                }
            }
            _draw.SetPixels(cols, mip);
        }

        _draw.Apply();
        heightMap.sprite = Sprite.Create(_draw, new Rect(0, 0, _draw.width, _draw.height), Vector2.one * 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
