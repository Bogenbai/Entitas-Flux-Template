using System.Text;
using Entitas;

namespace Code.Common.Entity.ToStrings
{
  public class EntityPrinter
  {
    private string _oldBaseToStringCache;
    private string _toStringCache;
    private readonly StringBuilder _toStringBuilder = new();

    private readonly INamedEntity _entity;

    public EntityPrinter(INamedEntity entity)
    {
      _entity = entity;
    }

    public string BuildToString()
    {
      if (_toStringCache != null && _oldBaseToStringCache == _entity.BaseToString())
        return _toStringCache;

      _toStringBuilder.Clear();

      IComponent[] components = _entity.GetComponents();

      if (components.Length == 0)
      {
        _toStringCache = "No components";
        return _toStringCache;
      }

      _toStringBuilder.Append($"{_entity.EntityName(components)}");

      _toStringBuilder.Append($" (retained {_entity.retainCount} times)");

      _toStringCache = _toStringBuilder.ToString();
      _oldBaseToStringCache = _entity.BaseToString();

      return _toStringCache;
    }

    public void InvalidateCache()
    {
      if (_oldBaseToStringCache != _entity.BaseToString())
        _toStringCache = null;
    }
  }
}