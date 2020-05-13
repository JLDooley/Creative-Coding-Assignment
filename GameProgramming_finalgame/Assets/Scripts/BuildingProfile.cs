using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BuildingProfile", order = 100)]
public class BuildingProfile : ScriptableObject
{
    public Mesh[] groundBlocks;
    public Mesh[] mainBlocks;
    public Mesh[] roofBlocks;

    public Mesh[] tunnelBlocks; //When a road intersects a tower, use these blocks instead

    public Material[] groundMaterials;
    public Material[] mainMaterials;
    public Material[] roofMaterials;



    public int maxHeight = 5;

}
