using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace TensorFlowLite
{
    public class ClassifyCore : BaseImagePredictor<float>
    {
        float[,] outputs;
        string[] labels={"Ashina_Disease", "Ashina_Packet_Disease", "Bari4_Disease", "Katimon_Disease", "Khirsapat_Disease", "Langra_Disease"};

        int outputWidth, outputHeight;

        public ClassifyCore(string modelPath, bool useGpu = true) : base(modelPath, Accelerator.GPU)
        {
            outputs = new float[1,6];
        }

        public override void Invoke(Texture inputTex)
        {
            
            Debug.Log("tensor input Info "+ interpreter.GetInputTensorInfo(0));
            Debug.Log("tensor output Info "+ interpreter.GetOutputTensorInfo(0));
            Debug.Log("Texture Info"+ inputTex.width);

            ToTensor(inputTex, inputTensor);
            Debug.Log("Tensor Info "+ inputTensor);
            interpreter.SetInputTensorData(0, inputTensor);
            // var result = interpreter.Invoke();
            // Debug.Log("Result" + result);
            interpreter.Invoke();
            var size= interpreter.GetOutputTensorInfo(0);
            Debug.Log(size);
            interpreter.GetOutputTensorData(0, outputs);
        }

        public string GetResults()
        {
            int max=0;
            float pre=-100;
            for (int i=0; i<6; i++)
            {
                if(outputs[0,i]>=pre)
                {
                    pre=outputs[0,i];
                    max=i;
                }
                Debug.Log(outputs[0,i]+1+"\n");
            }
            int highest=max+1;
            Debug.Log("max= "+highest);
            Debug.Log("Image is " + labels[max]);
            return labels[max];
            // float one = outputs[0,0];
            // float two = outputs[0,1];
            // float three = outputs[0,2];
            // float four = outputs[0,3];
            // float five = outputs[0,4];
            // float six = outputs[0,5];

        }

        public override void Dispose()
        {
            // resultBuffer.Release();

            // resultTexture.Release();
            // Object.Destroy(resultTexture);

            base.Dispose();
        }
    }
}
