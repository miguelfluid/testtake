using System;
using System.Collections.Generic;
using System.Text;
using TestTake.Core.Base;
using TestTake.Views.BusinessCrud;
using TestTake.Views.BusinessList;

namespace TestTake.Core.Business
{
  public class Room: BaseEntity
  {
    public string Name { get; set; }

    public ViewCrudRoom GetViewCrud()
    {
      return new ViewCrudRoom()
      {
        Id = Id,
        Name = Name
      };
    }

    public ViewListRoom GetViewList()
    {
      return new ViewListRoom()
      {
        Id = Id,
        Name = Name
      };
    }

  }
}
