using System.Collections;
using UnityEngine;
namespace VTheater.Dance.Handler {
    public class MakeSpectrum : MonoBehaviour {
        private static int resolution = 8192;
        private Texture2D spectrumTex;
        private Color[] spectrumCol;
        [SerializeField] private RenderTexture renderTex;
        [SerializeField] private Material spectrumMaterial;
        [SerializeField] private Mesh mainMesh;
        [SerializeField] private int channelNum = 0;
        [SerializeField] private float multiVal = 200.0f;
        [SerializeField] private float multiPow = 200.0f;
        private float[] audioSample;

        void Start () {
            spectrumTex = new Texture2D (resolution, 1, TextureFormat.RFloat, false);
            spectrumCol = new Color[resolution];
            audioSample = new float[resolution];
        }

        void Update () {
            AudioListener.GetSpectrumData (audioSample, channelNum, FFTWindow.BlackmanHarris);
            for (int index = 0; index < resolution; index++) {
                spectrumCol[index] = new Color (Mathf.Pow (audioSample[index], multiPow) * multiVal, 0.0f, 0.0f, 1.0f);
            }
            spectrumTex.filterMode = FilterMode.Point;
            spectrumTex.SetPixels (spectrumCol);
            spectrumTex.Apply ();
            Graphics.Blit (spectrumTex, renderTex);
        }

        void OnRenderObject () {
            if (mainMesh != null && spectrumMaterial != null) {
                spectrumMaterial.SetTexture ("_MainTex", spectrumTex);
                spectrumMaterial.SetPass (0);
                Graphics.DrawMeshNow (mainMesh, transform.position, transform.rotation);
            }
        }

        public float returnVal (float minLength, float maxLength) {
            int minIndex = (int) Mathf.Floor (resolution * minLength);
            minIndex = Mathf.Clamp (minIndex, 0, resolution - 1);
            int maxIndex = (int) Mathf.Floor (resolution * maxLength);
            maxIndex = Mathf.Clamp (maxIndex, 0, resolution - 1);

            if (minLength >= maxLength) return audioSample[minIndex];
            float val = 0.0f;
            for (int index = minIndex; index < maxIndex; index++) {
                val += audioSample[index];
            }
            val /= (maxLength - minLength);
            return val;
        }
    }
}