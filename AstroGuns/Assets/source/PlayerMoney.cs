using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : ScriptableObject
{
    static List<Tuple<double, string>> MoneySymbols = new List<Tuple<double, string>>()
    {
        Tuple.Create<double, string>( 1, "" ),
        Tuple.Create<double, string>( 1000, "k" ),
        Tuple.Create<double, string>( 1000000, "m" ),
        Tuple.Create<double, string>( 1000000000, "b" ),
        Tuple.Create<double, string>( 1000000000000, "t" ),
    };

    public UnityEvent OnValueUpdated = new UnityEvent();

    public double ActualValue { get; private set; }
    /*
    public static PlayerMoney operator +(PlayerMoney a, PlayerMoney b)
    {
        return new PlayerMoney(a.ActualValue + b.ActualValue);
    }
    */

    public void Add(double amount)
    {
        ActualValue += amount;
        if(Math.Abs(amount) > 0.0001)
            OnValueUpdated.Invoke();
    }

    public void Add(PlayerMoney amount)
    {
        Add(amount.ActualValue);
    }

    public override string ToString()
    {
        return GetMoneyString(ActualValue);
    }

    public static string GetMoneyString(double value)
    {
        Tuple<double, string> T = MoneySymbols.Find((Tuple<double, string> X) => { return X.Item1 * 1000 >= value; });

        if(T == null)
            return value.ToString();

        double valueToReturn = Math.Abs((value / T.Item1));
        int length = (int)(Math.Log10(valueToReturn) + 1);

        return string.Format(
            length <= 1 ? "{0:N2}" : 
            length == 2 ? "{0:N1}" : 
            "{0:N0}",  
            valueToReturn) + T.Item2;

    }
}
