using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System.Linq;


public class T001 : MonoBehaviour {

	[MenuItem("Tools/Extract")]
	public static void ExtractSprite()
	{
        List<Sprite> Spritelist = new List<Sprite>();
        Texture2D[] tex2d = Selection.GetFiltered<Texture2D>(SelectionMode.DeepAssets);
        foreach (Texture2D tex in tex2d)
        {
            Spritelist.AddRange(AssetDatabase.LoadAllAssetsAtPath
                (AssetDatabase.GetAssetPath(tex)).OfType<Sprite>());
        }

        
	}
}
