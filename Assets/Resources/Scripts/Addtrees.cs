using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addtrees : MonoBehaviour
{
    public Terrain WorldTerrain;
    public LayerMask TerrainLayer;
    public LayerMask WaterLayer;
    public static float TerrainLeft, TerrainRight, TerrainTop, TerrainBottom, TerrainWidth, TerrainLength, TerrainHeight;

    public int surfaceIndex = 0;

    private TerrainData terrainData;

    public void Awake()
    {
        terrainData = WorldTerrain.terrainData;

        TerrainLeft = WorldTerrain.transform.position.x;
        TerrainBottom = WorldTerrain.transform.position.z;
        TerrainWidth = WorldTerrain.terrainData.size.x;
        TerrainLength = WorldTerrain.terrainData.size.z;
        TerrainHeight = WorldTerrain.terrainData.size.y;
        TerrainRight = TerrainLeft + TerrainWidth;
        TerrainTop = TerrainBottom + TerrainLength;

        InstantiateRandomPosition("TreePrefabs/PFConiferSmallBOTD", 400, 0f);
        InstantiateRandomPosition("TreePrefabs/PFConiferMediumBOTD", 400, 0f);
        InstantiateRandomPosition("TreePrefabs/PFConiferTallBOTD", 400, 0f);
        InstantiateRandomPosition("TreePrefabs/PFConiferBareBOTD", 400, 0f);
        InstantiateRandomPosition("Conifers/RockMeshVariant", 200, 0f);
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
                //Debug.Log(terrainHeight);
            }

            Vector3 location = new Vector3(RandomPositionX, terrainHeight, RandomPositionZ);

            surfaceIndex = GetMainTexture(location);
            print("index: " + surfaceIndex.ToString() + ", name: " + terrainData.terrainLayers[surfaceIndex].name);

            if (terrainHeight > 104 && terrainData.terrainLayers[surfaceIndex].name == "Grass")
            {
                RandomPositionY = terrainHeight + AddedHeight;

                randomPosition = new Vector3(RandomPositionX, RandomPositionY, RandomPositionZ);

                GameObject newTree = (GameObject)Instantiate(Resources.Load(Resourse, typeof(GameObject)), randomPosition, Quaternion.identity);
                newTree.transform.parent = transform;
            }

        } while (i < Amount);
        
    }

    private float[] GetTextureMix(Vector3 WorldPos)
    {

        int mapX = (int)(((WorldPos.x - TerrainLeft) / TerrainWidth) * terrainData.alphamapWidth);
        int mapZ = (int)(((WorldPos.z - TerrainBottom) / TerrainLength) * terrainData.alphamapHeight);

        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];

        for (int n = 0; n < cellMix.Length; n++)
        {
            cellMix[n] = splatmapData[0, 0, n];
        }
        return cellMix;
    }

    private int GetMainTexture(Vector3 WorldPos)
    {

        float[] mix = GetTextureMix(WorldPos);

        float maxMix = 0;
        int maxIndex = 0;

        for (int n = 0; n < mix.Length; n++)
        {
            if (mix[n] > maxMix)
            {
                maxIndex = n;
                maxMix = mix[n];
            }
        }
        return maxIndex;
    }
}
