using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
#if UNITY_4_6
// Unity 4.6.xではAnimatorControllerクラスがUnityEditorInternal名前空間で定義されている
using UnityEditorInternal;
#else
using UnityEditor.Animations;
#endif
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class SpriteAnimationCreator : MonoBehaviour {
    // デフォルトのフレーム間隔を定義する
    private static float defaultInterval = 0.1f;
    
    // Assetsメニュー→「Create」に「Sprite Animation」の項目を追加する
    [MenuItem("Assets/Create/Sprite Animation")]
    public static void Create()
    {
        // Projectビューで選択されているスプライトを取得する
        List<Sprite> selectedSprites = new List<Sprite>();
        //	Selection.GetFiltered(typeof(Sprite), SelectionMode.DeepAssets)
        //	.OfType<Sprite>());

        //Debug.Log(selectedSprites.Count);
        
        // テクスチャが選択されている場合は、その中のスプライトを取得する
        Object[] selectedTextures = 
        	Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        foreach(Object texture in selectedTextures)
        {
            selectedSprites.AddRange(AssetDatabase.LoadAllAssetsAtPath(
            	AssetDatabase.GetAssetPath(texture)).OfType<Sprite>());
        }
        
        // スプライトが選択されていなければエラー
        if(selectedSprites.Count < 1)
        {
            Debug.LogWarning("No sprite selected.");
            return;
        }
        
        // スプライトの末尾の連番でソートする
        string suffixPattern = "_?([0-9]+)$";
        selectedSprites.Sort((Sprite _1, Sprite _2)=>{
            Match match1 = Regex.Match(_1.name, suffixPattern);
            Match match2 = Regex.Match(_2.name, suffixPattern);
            if(match1.Success && match2.Success)
            {
                return (int.Parse(match1.Groups[1].Value) - 
                	int.Parse(match2.Groups[1].Value));
            }
            else
            {
                return _1.name.CompareTo(_2.name);
            }
        });
        
        // 最初のスプライトのあるフォルダに後でアセットを保存する
        string baseDir = 
        	Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedSprites[0]));
        // アニメーションの名前は最初のスプライトの連番なしの名前にする
        string baseName = Regex.Replace(selectedSprites[0].name, suffixPattern, "");
        if(string.IsNullOrEmpty(baseName))
        {
            baseName = selectedSprites[0].name;
        }
        
        // カンバスがなければ作成する
        Canvas canvas = FindObjectOfType<Canvas>();
        if(canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            canvasObj.layer = LayerMask.NameToLayer("UI");
        }
        
        // イメージを作成する
        GameObject obj = new GameObject(baseName);
        obj.transform.parent = canvas.transform;
        obj.transform.localPosition = Vector3.zero;
        
        Image image = obj.AddComponent<Image>();
        image.sprite = (Sprite)selectedSprites[0];
        image.SetNativeSize();
        
        // Animatorコンポーネントをアタッチする
        Animator animator = obj.AddComponent<Animator>();
        
        // アニメーションクリップを作成する
        AnimationClip animationClip = 
        	AnimatorController.AllocateAnimatorClip(baseName);
        
#if UNITY_4_6
        // Unity 4.6.xではアニメーションタイプをModelImporterAnimationType.Genericにする
        AnimationUtility.SetAnimationType(
        	animationClip, ModelImporterAnimationType.Generic);
#endif
        
        // EditorCurveBindingを使用して、キーフレームとイメージのSpriteプロパティを関連づける
        EditorCurveBinding editorCurveBinding = new EditorCurveBinding();
        editorCurveBinding.type = typeof(Image);
        editorCurveBinding.path = "";
        editorCurveBinding.propertyName = "m_Sprite";
        
        // 選択されたスプライトの数分キーフレームを作成して、各キーフレームにスプライトを割り当てる
        ObjectReferenceKeyframe[] keyFrames = 
        	new ObjectReferenceKeyframe[selectedSprites.Count];
        for(int i=0; i<selectedSprites.Count; i++)
        {
            keyFrames[i] = new ObjectReferenceKeyframe();
            keyFrames[i].time = i * defaultInterval;
            keyFrames[i].value = selectedSprites[i];
        }
        
        AnimationUtility.SetObjectReferenceCurve(
        	animationClip, editorCurveBinding, keyFrames);
        
        // Loop Timeプロパティはスクリプトから直接設定することができないため、
        // SerializedPropertyを使って設定する
        // （この方法はUnityの将来のバージョンで使えなくなる可能性があります）
        SerializedObject serializedAnimationClip = 
        	new SerializedObject(animationClip);
        SerializedProperty serializedAnimationClipSettings = 
        	serializedAnimationClip.FindProperty("m_AnimationClipSettings");
        serializedAnimationClipSettings
        	.FindPropertyRelative("m_LoopTime").boolValue = true;
        serializedAnimationClip.ApplyModifiedProperties();
        
        // 作成したアニメーションクリップをアセットとして保存する
        SaveAsset(animationClip, baseDir + "/" + baseName + ".anim");
        
        // アニメーターコントローラーを作成する
        AnimatorController animatorController = 
        	AnimatorController.CreateAnimatorControllerAtPathWithClip(
        	baseDir + "/" + baseName + ".controller", animationClip);
        animator.runtimeAnimatorController = 
        	(RuntimeAnimatorController)animatorController;
    }
    
    // アセットとして保存するための関数。既存のものがある場合は、上書きして更新する
    private static void SaveAsset(Object obj, string path)
    {
        Object existingAsset = AssetDatabase.LoadMainAssetAtPath(path);
        if(existingAsset != null)
        {
            EditorUtility.CopySerialized(obj, existingAsset);
            AssetDatabase.SaveAssets();
        }
        else
        {
            AssetDatabase.CreateAsset(obj, path);
        }
    }
}
