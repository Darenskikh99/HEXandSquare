using UnityEngine;

public class HexMetrics : MonoBehaviour
{
    public const float outerRadius = 10f;

    public const float innerRadius = outerRadius * 0.866f;

    public const float solidFactor = 0.75f;

    public const float blendFactor = 1f - solidFactor;

    public const float elevationStep = 5f;

    public const int terrscesPerSlop = 2;

    public const int terracesSteps = terrscesPerSlop * 2 + 1;

    public const float horrizontalTerraceStepSize = 1f / terracesSteps;

    public const float verticalTerraceStepSize = 1f / (terrscesPerSlop + 1);

    public static Vector3[] corners =
    {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
    };



    public static Vector3 GetFirstCorner (HexDirection direction)
    {
        return corners[(int)direction];
    }

    public static Vector3 GetSecondCorner (HexDirection direction)
    {
        return corners[(int)direction + 1];
    }

    public static Vector3 GetFirstSolidCorner (HexDirection direction)
    {
        return corners[(int)direction] * solidFactor;
    }

    public static Vector3 GetSecondSolidCorner (HexDirection direction)
    {
        return corners[(int)direction + 1] * solidFactor;
    }

    public static Vector3 GetBridge (HexDirection direction)
    {
        return (corners[(int)direction] + corners[(int)direction + 1]) * blendFactor;
    }

    public static Vector3 TerraceLerp (Vector3 a, Vector3 b, int step)
    {
        float horizontal = step * HexMetrics.horrizontalTerraceStepSize;
        a.x += (b.x - a.x) * horizontal;
        a.z += (b.z - a.z) * horizontal;
        float vertical = ((step + 1) / 2) * HexMetrics.horrizontalTerraceStepSize;
        a.y += (b.y - a.y) * vertical;
        return a;
    }

    public static Color TerraceLerp (Color a, Color b, int step)
    {
        float horizontal = step * HexMetrics.horrizontalTerraceStepSize;
        return Color.Lerp(a, b, horizontal);
    }

    public static HexEdgeType GetEdgeType(int elevation1, int elevation2)
    {
        if (elevation1 == elevation2)
        {
            return HexEdgeType.Flat;
        }

        int delta = elevation2 - elevation1;
        
        if(delta == 1 || delta == -1)
        {
            return HexEdgeType.Slope;
        }
        return HexEdgeType.Cliff;
    }
}
