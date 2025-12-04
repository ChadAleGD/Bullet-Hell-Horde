using System;
using System.Collections.Generic;






public readonly struct BlackboardKey
{
    private readonly string _keyName;
    private readonly int _hashedKey;



    public BlackboardKey(string keyName)
    {
        _keyName = keyName;
        _hashedKey = _keyName.GetHashCode();
    }


    public static bool operator ==(BlackboardKey left, BlackboardKey right) => left._hashedKey == right._hashedKey;
    public static bool operator !=(BlackboardKey left, BlackboardKey right) => !(left == right);
    public override bool Equals(object obj) => _hashedKey == obj.GetHashCode();
    public override int GetHashCode() => _hashedKey;
}



public class BlackboardValue<T>
{
    public string Name;
    public T Value;
    public Type ValueType;

    public BlackboardValue(string entryName, T value)
    {
        Name = entryName;
        Value = value;
        ValueType = typeof(T);
    }
}




[Serializable]
public class Blackboard
{

    private Dictionary<string, BlackboardKey> _stringKeyEntries = new();
    private Dictionary<BlackboardKey, object> _keyEntryValues = new();


    //-----------------------------------------------------------------------------------------//
    // Keys

    public BlackboardKey TryGetOrAddKey(string entryName)
    {
        if (_stringKeyEntries.TryGetValue(entryName, out var foundKey)) return foundKey;

        var newKey = new BlackboardKey(entryName);
        _stringKeyEntries[entryName] = newKey;
        return newKey;
    }


    //-----------------------------------------------------------------------------------------//




    public bool TryGetValue<T>(BlackboardKey key, out T value)
    {
        if (_keyEntryValues.TryGetValue(key, out var objectValue) && objectValue is BlackboardValue<T> castedValue)
        {
            value = castedValue.Value;
            return true;
        }

        value = default;
        return false;
    }


    public void ModifyValue<T>(BlackboardKey key, T value) => _keyEntryValues[key] = value;



    public string DebugValue(BlackboardKey key)
    {
        if (_keyEntryValues.TryGetValue(key, out var value)) return $"Value: {value}";

        else return $"Error retrieving value for key";

    }



}