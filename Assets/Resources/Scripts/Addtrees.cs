using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addtrees : MonoBehaviour
{
    public Terrain WorldTerrain;
    public LayerMask TerrainLayer;
    public static float TerrainLeft, TerrainRight, TerrainTop, TerrainBottom, TerrainWidth, TerrainLength, TerrainHeight;

    public void Awake()
    {
        TerrainLeft = WorldTerrain.transform.position.x;
        TerrainBottom = WorldTerrain.transform.position.z;
        TerrainWidth = WorldTerrain.terrainData.size.x;
        TerrainLength = WorldTerrain.terrainData.size.z;
        TerrainHeight = WorldTerrain.terrainData.size.y;
        TerrainRight = TerrainLeft + TerrainWidth;
        TerrainTop = TerrainBottom + TerrainLength;

        InstantiateRandomPosition("Conifers/RenderPipelineSupport/BuiltinRP/Prefabs/PFConiferSmallBOTD", 500, 0f);
        InstantiateRandomPosition("Conifers/RenderPipelineSupport/BuiltinRP/Prefabs/PFConiferMediumBOTD", 500, 0f);
        InstantiateRandomPosition("Conifers/RenderPipelineSupport/BuiltinRP/Prefabs/PFConiferTallBOTD", 500, 0f);
        InstantiateRandomPosition("Conifers/RenderPipelineSupport/BuiltinRP/Prefabs/PFConiferBareBOTD", 500, 0f);
    }

    public void InstantiateRandomPosition(string Resourse, int Amount, float AddedHeight)
    {
        var i = 0;
        float terrainHeight = 0f;
        RaycastHit hit;
        float RandomPositionX, RandomPositionY, RandomPositionZ;
        Vector3 randomPosition = Vector3.zero;

        do
        {
            i++;
            RandomPositionX = Random.Range(TerrainLeft, TerrainRight);
            RandomPositionZ = Random.Range(TerrainBottom, TerrainTop);

            if(Physics.Raycast(new Vector3(RandomPositionX, 9999f, RandomPositionZ), Vector3.down, out hit, Mathf.Infinity, TerrainLayer))
            {
                terrainHeight = hit.point.y;
            }

            RandomPositionY = terrainHeight + AddedHeight;

            randomPosition = new Vector3(RandomPositionX, RandomPositionY, RandomPositionZ);

            Instantiate(Resources.Load(Resourse, typeof(GameObject)), randomPosition, Quaternion.identity);

        } while (i < Amount);
        
    }
}
