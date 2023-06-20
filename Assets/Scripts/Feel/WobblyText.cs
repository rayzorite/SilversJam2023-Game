using System;
using TMPro;
using UnityEngine;

namespace Feel
{
    public class WobblyText : MonoBehaviour
    {
        public TextMeshPro textComponent;

        private void Update()
        {
            textComponent.ForceMeshUpdate();
            var textInfo = textComponent.textInfo;

            for (var i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;
                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (var j = 0; j < 4; j++)
                {
                    var orig = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 0.25f, 0);
                }
            }
            
            for (var i = 0; i < textInfo.meshInfo.Length; i++)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponent.UpdateGeometry(meshInfo.mesh, i);
            }
        }
    }
}
