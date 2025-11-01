using UnityEngine;
using UnityEngine.UI;

public enum RequirementType
{
    Level,
    Time
}

public class Requirement
{
    public RequirementType type;
    public int value;
}

public class Plant
{
    public string name;
    public Sprite image;

    public Requirement[] requirements;
}

public class PlacedPlant
{
    public PlacedPlant(Plant plantType, Vector3 fixedPos)
    {
        this.plantType = plantType;
        this.fixedPos = fixedPos;

        this.obj = new();
        this.obj.AddComponent<SpriteRenderer>();
        this.obj.GetComponent<SpriteRenderer>().sprite = plantType.image;

        this.obj.transform.position = fixedPos;
    }

    public Plant plantType;
    public GameObject obj;
    public Vector3 fixedPos;
    public Vector3 movePos;
}
