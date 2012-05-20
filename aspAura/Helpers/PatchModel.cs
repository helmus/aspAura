using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

public class PatchModel<T> where T: class
{
    private Dictionary<string, object> _memberData;
    private List<string> _keys;

    public PatchModel( Dictionary<string, object> memberData) 
    {
        _memberData = memberData;
    }

    private List<string> GetEntityKeyNames<TEntity>(DbContext context) where TEntity : class
    {
        var set = ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet<TEntity>();
        var entitySet = set.EntitySet;
        return entitySet.ElementType.KeyMembers.Select(k => k.Name).ToList() ;
    }


    /// <summary>
    /// Gets the model keys
    /// </summary>
    /// <param name="db">dbcontext is required to determine the keys</param>
    /// <returns></returns>
    public Object[] GetKeys(DbContext db)
    {
        _keys =  GetEntityKeyNames<T>(db);
        var keyValues = new List<object>();
        foreach (var key in _keys)
        {
            var propInfo = typeof(T).GetProperty(key, BindingFlags.Public | BindingFlags.Instance);
            keyValues.Add(Convert.ChangeType(_memberData[key], propInfo.PropertyType));
        }
        return keyValues.ToArray();
    }

    /// <summary>
    /// Use GetKeys to retrieve the keys, load the model from db, then update the model, then validate 
    /// </summary>
    /// <param name="model">The attached model to update</param>
    public void UpdateModel(T model)
    {
        foreach (var member in _memberData)
        {
            var propInfo = typeof (T).GetProperty(member.Key, BindingFlags.Public | BindingFlags.Instance );
            if(!_keys.Contains(member.Key)){
                // if the datatype does not match an error will occur, 
                // it's up to the client side code ( NOT the user ) to only send valid data.
                propInfo.SetValue(model, Convert.ChangeType(member.Value, propInfo.PropertyType), null);
            }
        }
    }
}
