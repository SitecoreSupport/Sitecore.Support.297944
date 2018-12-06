using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Grid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.XA.Foundation.Grid.Repositories
{
  public class GridSettingsRepository: Sitecore.XA.Foundation.Grid.Repositories.GridSettingsRepository
  {
    public override IEnumerable<GridOption> CreateGridModel(Item definitionItem, string values)
    {
      #region FIX 297944
      string valuesDecoded = values;
      while (valuesDecoded.Contains("%25"))
      {
        valuesDecoded = HttpUtility.UrlDecode(valuesDecoded);
      }
      if (valuesDecoded.Contains("%7B"))
      {
        valuesDecoded = HttpUtility.UrlDecode(valuesDecoded);
      }
      #endregion

      List<GridOption> list = CreateGridModel(definitionItem).ToList();
      if (string.IsNullOrEmpty(values))
      {
        return list;
      }
      foreach (GridOption item in list)
      {
        PopulateValues(item, valuesDecoded);
      }
      return list;
    }
  }
}