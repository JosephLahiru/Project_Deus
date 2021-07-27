using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addtrees : MonoBehaviour
{
    public Terrain WorldTerrain;
    public LayerMask TerrainLayer;
    public LayerMask WaterLayer;
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

            if(terrainHeight > 104)
            {
                RandomPositionY = terrainHeight + AddedHeight;

                randomPosition = new Vector3(RandomPositionX, RandomPositionY, RandomPositionZ);

                GameObject newTree = (GameObject)Instantiate(Resources.Load(Resourse, typeof(GameObject)), randomPosition, Quaternion.identity);
                newTree.transform.parent = transform;
            }

        } while (i < Amount);
        
    }
}
