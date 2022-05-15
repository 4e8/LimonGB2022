using NFG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFG
{
    public class RuntimeExample : MonoBehaviour
    {
        public NaturalFormationGenerator generator;

        void Start()
        {
            generator.DoGenerationRuntime();
        }
    }
}