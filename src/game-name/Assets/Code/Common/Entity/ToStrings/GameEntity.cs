using System;
using System.Collections.Generic;
using System.Text;
using Code.Common.Entity.ToStrings;
using Entitas;

// ReSharper disable once CheckNamespace
public sealed partial class GameEntity : INamedEntity
{
    private EntityPrinter _printer;

    private static readonly Dictionary<string, Func<GameEntity, string>> _componentPrinters = new()
    {
        { nameof(Code.Common.Name), entity => entity.PrintName() },
    };

    public override string ToString()
    {
        _printer ??= new EntityPrinter(this);

        _printer.InvalidateCache();

        return _printer.BuildToString();
    }

    public string EntityName(IComponent[] components)
    {
        if (components.Length == 0)
            return "Unknown";

        foreach (IComponent component in components)
        {
            if (_componentPrinters.TryGetValue(component.GetType().Name, out var printer))
            {
                return printer(this);
            }
        }
        
        if (hasName)
            return Name;

        return components[0].GetType().Name;
    }
    
    private string PrintName() => BuildEntityString(hasName ? $"{Name}" : null, hasId ? $"Id:{Id}" : null);

    private string BuildEntityString(string entityType, params string[] parts)
    {
        StringBuilder stringBuilder = new(entityType);
        foreach (string part in parts)
        {
            if (!string.IsNullOrEmpty(part))
            {
                stringBuilder.Append(" ").Append(part);
            }
        }
        return stringBuilder.ToString();
    }

    public string BaseToString() => base.ToString();
}

