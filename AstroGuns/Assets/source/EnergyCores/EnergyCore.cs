using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class EnergyCore
{
    [System.Serializable]
    public enum EnergyCoreType
    {
        None,
        Profit,
        Haste,
        Fortune
    };

    private static readonly Dictionary<EnergyCoreType, string> TypeNames = new Dictionary<EnergyCoreType, string>()
    {
        {EnergyCoreType.Haste, "Haste core" },
        {EnergyCoreType.Profit, "Profit core" },
        {EnergyCoreType.Fortune, "Fortune core" }
    };

    private static readonly List<float> Values = new List<float> { 0.05f, 0.1f, 0.25f, 0.5f, 1f };

    public string Name
    {
        get {
            return TypeNames[Type];
        }
    }

    public string Description
    {
        get {
            return "!!";
        }
    }

    public EnergyCoreType Type;
    public int Level;
    public float Value { get => Values[Level]; }

}
