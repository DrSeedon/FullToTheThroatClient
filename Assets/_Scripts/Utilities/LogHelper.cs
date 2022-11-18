using System;
using System.Linq.Expressions;
using UnityEngine;

public class LogHelper
{
    //метод для дебага, вывода названия переменной и её значения
    //LogHelper.Log(() => value);
    //public static void Log<T>(Expression<Func<T>> value)
    public static void Log<T>(Expression<Func<T>> value)
    {
        var me = (MemberExpression) value.Body;
        var variableName = me.Member.Name;
        var variableValue = value.Compile()();

        Debug.Log(variableName + " => " + variableValue);
    }
}
