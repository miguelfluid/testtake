using TestTake.Core.Base;
using TestTake.Views.BusinessCrud;
using TestTake.Views.BusinessList;

namespace TestTake.Core.Business
{
  public class User : BaseEntity
  {
    public string Name { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
    public ViewListUser GetViewList()
    {
      return new ViewListUser()
      {
        Id = Id,
        Name = Name
      };
    }

    public ViewCrudUser GetViewCrud()
    {
      return new ViewCrudUser()
      {
        Id = Id,
        Name = Name,
        Mail = Mail
      };
    }
  }
}
