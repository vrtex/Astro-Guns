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

    public static List<EnergyCore> availibleCores = new List<EnergyCore>();

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
            return "EEEE";
        }
    }

    public static void AddCore(EnergyCore toAdd)
    {
        availibleCores.Add(toAdd);
    }

    public static EnergyCore PollCore(int level, EnergyCoreType type)
    {
        int foundIndex = availibleCores.FindIndex((EnergyCore e) => e.Type == type && e.Level == level);
        if(foundIndex < 0)
            return null;
        EnergyCore found = availibleCores[foundIndex];
        availibleCores.RemoveAt(foundIndex);
        return found;
    }

    public static int CountCores(int level, EnergyCoreType type)
    {
        return availibleCores.Count((EnergyCore e) => e.Level == 0 && e.Type == type);
    }

    public EnergyCoreType Type;
    public int Level;
    public float Value { get => Values[Level]; }

}
