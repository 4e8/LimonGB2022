using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NFG
{
    public class WorldGrower : MonoBehaviour
    {
        public bool growOnStart = false;

        public float layerGrowLength;

        public Transform[] layers;

        Dictionary<int, List<GameObject>> layerData = new Dictionary<int, List<GameObject>>();
        Dictionary<int, List<Vector3>> layerScaleData = new Dictionary<int, List<Vector3>>();

        public WorldGrower nextWorld;

        // Use this for initialization
        void Start()
        {
            if (growOnStart)
            {
                DoGrowth();
            }
        }

        void DoGrowth()
        {
            layers = GetComponentsInChildren<Transform>().Where(t => t != transform && t.parent == transform).ToArray();
            for (int i = 0; i < layers.Length; i++)
            {
                for (int j = 0; j < layers[i].transform.childCount; j++)
                {
                    GameObject prefab = layers[i].transform.GetChild(j).gameObject;
                    if (layerData.ContainsKey(i))
                    {
                        layerData[i].Add(prefab);
                        layerScaleData[i].Add(prefab.transform.localScale);
                    }
                    else
                    {
                        layerData.Add(i, new List<GameObject>() { prefab });
                        layerScaleData.Add(i, new List<Vector3>() { prefab.transform.localScale });
                    }

                    prefab.transform.localScale = new Vector3(0, 0, 0);
                    //prefab.SetActive(false);
                }
            }
            StartCoroutine("GrowWorld");
        }

        // Update is called once per frame
        IEnumerator GrowWorld()
        {
            yield return new WaitForSeconds(1);
            Vector3 velocity = new Vector3(0, 0, 0);
            for (int i = 0; i < layers.Length; i++)
            {
                float time = 0;
                while (time < layerGrowLength)
                {
                    for (int j = 0; j < layerData[i].Count; j++)
                    {
                        layerData[i][j].transform.localScale = Vector3.MoveTowards(layerData[i][j].transform.localScale, layerScaleData[i][j], (layerGrowLength * 2) * Time.deltaTime);
                    }
                    yield return new WaitForEndOfFrame();
                    time += Time.deltaTime;

                }
            }
            StartCoroutine("ShrinkWorld");
        }

        IEnumerator ShrinkWorld()
        {
            yield return new WaitForSeconds(3);
            Vector3 velocity = new Vector3(0, 0, 0);
            for (int i = layers.Length - 1; i >= 0; i--)
            {
                float time = 0;
                while (time < layerGrowLength)
                {
                    for (int j = 0; j < layerData[i].Count; j++)
                    {
                        layerData[i][j].transform.localScale = Vector3.MoveTowards(layerData[i][j].transform.localScale, Vector3.zero, (layerGrowLength * 2) * Time.deltaTime);
                    }
                    yield return new WaitForEndOfFrame();
                    time += Time.deltaTime;

                }
            }
            if (nextWorld)
            {
                nextWorld.gameObject.SetActive(true);
                nextWorld.DoGrowth();
                gameObject.SetActive(false);
            }

        }
    }
}