using TensorFlowLite;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Classify : MonoBehaviour
{
    [SerializeField, FilePopup("*.tflite")] string fileName = "";
    // [SerializeField] Texture2D inputTex;

    // [SerializeField] RawImage inputImage;
    // [SerializeField] ComputeShader compute;
    [SerializeField] RawImage imageTexture;
    ClassifyCore classifyCore;
    public Text disName;
    void Start()
    {
        classifyCore = new ClassifyCore(fileName /*,compute*/);
        
        // Debug.Log("inputTex "+inputTex);   
    }
    public void predict()
    {
        // classifyCore.Invoke(inputTex);
        classifyCore.Invoke(imageTexture.texture);
        string name=classifyCore.GetResults();
        disName.text=name;
        // Debug.Log("bhjghhgkmsdfjhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
    }

    void OnDestroy()
    {
        classifyCore?.Dispose();
    }

}
