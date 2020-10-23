using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Shot : ScriptableObject
{
    public enum Type { Push, Slow, Stop,Disable};
    public enum Type2 { Push, Slow, Stop,Disable};
    
    [SerializeField] 
    string Name;
    [SerializeField] 
    Type BaseType;
    [SerializeField] 
    Type2 SecondType;
    [SerializeField] 
    float Damage;
    public string GetName()
    {
        return Name;
    }
    public string GetMyType()
    {
        return System.Enum.GetName(typeof(Type), BaseType);
    }
    public string GetType2()
    {
        return System.Enum.GetName(typeof(Type2), SecondType);
    }
    public float GetDamage()
    {
        return Damage;
    }
}