using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : ScriptableObject
{
    static List<Tuple<double, string>> MoneySymbols = new List<Tuple<double, string>>()
    {
        Tuple.Create<double, string>( 1000, "k" ),
        Tuple.Create<double, string>( 1000000, "m" ),
        Tuple.Create<double, string>( 1000000000, "b" ),
        Tuple.Create<double, string>( 1000000000000, "t" ),
    };

    public double ActualValue;
    /*
    public static PlayerMoney operator +(PlayerMoney a, PlayerMoney b)
    {
        return new PlayerMoney(a.ActualValue + b.ActualValue);
    }
    */

    public void Add(double amount)
    {
        ActualValue += amount;
    }

    public void Add(PlayerMoney amount)
    {
        Add(amount.ActualValue);
    }

    public override string ToString()
    {
        Tuple<double, string> T = MoneySymbols.Find((Tuple<double, string> X) => { return X.Item1 * 1000 >= ActualValue; });

        if(T == null)
            return ActualValue.ToString();

        double valueToReturn = Math.Abs((ActualValue / T.Item1));
        int length = (int)(Math.Log10(valueToReturn) + 1);

        return string.Format(
            length <= 1 ? "{0:N2}" : 
            length == 2 ? "{0:N1}" : 
            "{0:N0}",  
            valueToReturn) + T.Item2;
    }
}
