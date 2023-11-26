using UnityEngine;

/// <summary>
/// Finds random points to spawn objects at
/// </summary>
public static class SpawnUtilities
{
    /// <summary>
    /// World coordinates of the lower-left corner of the screen.
    /// </summary>
    public static Vector3 Min;
    /// <summary>
    /// World coordinates of the upper-right corner of the screen
    /// </summary>
    public static Vector3 Max;

    /// <summary>
    /// Find the bounds of the screen in world coordinates
    /// This is called by the run-time system when we first try to call one of the methods below
    /// </summary>
    //static SpawnUtilities()
    //{
    //    Min = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
    //    Max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    //}

    /// <summary>
    /// Random point on the screen
    /// </summary>
    public static Vector3 RandomPoint()
    {
        var select = Random.Range(0, 2);
        float x;
        float y;
        float z;
        if (select == 0)
        {
            x = 1505;
            y = 60;
            z = -474;
            
        }
        else
        {
            x = -650;
            y = 60;
            z = 741;
        }
        Vector3 m = new Vector3(x, y, z);
        return m;

    }

    public static Vector3 RandomFreePoint(float radius)
    {
        var position = RandomPoint();
        for (var i = 0; i < 50 && !PointFree(position, radius); i++)
            position = RandomPoint();
        return position;
    }

    public static bool PointFree(Vector3 position, float radius)
    {
        return Physics2D.CircleCast(position, radius, Vector3.up, 0);
    }

}