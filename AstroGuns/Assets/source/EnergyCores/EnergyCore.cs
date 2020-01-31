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
            return
                Type == EnergyCoreType.Fortune ? "Bonus double spawn chance: " + Value * 100 + "%" :
                Type == EnergyCoreType.Profit ? "Profit increase " + Value * 100 + "%" :
                "Bonus forge speed " + Value * 100 + "%";
        }
    }

    public static string GetDescription(int level, EnergyCoreType Type)
    {
        return
                Type == EnergyCoreType.Fortune ? "Bonus double spawn chance: " + Values[level] * 100 + "%" :
                Type == EnergyCoreType.Profit ? "Profit increase " + Values[level] * 100 + "%" :
                "Bonus forge speed " + Values[level] * 100 + "%";
    }

    public EnergyCoreType Type;
    public int Level;
    public float Value { get => Values[Level]; }

}
