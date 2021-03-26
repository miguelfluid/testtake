using System.Collections.Generic;
using TestTake.Views.BusinessCrud;
using TestTake.Views.BusinessList;

namespace TestTake.Core.Interface
{
  public interface IServiceChat
  {
    public string NewUser(ViewCrudUser view);
    public ViewCrudUser GetUser(int id);
    public List<ViewListUser> ListUser(int pageSize, int page, string filter);
    public string NewRoom(ViewCrudRoom view);
    public List<ViewListRoom> ListRoom(int pageSize, int page, string filter);
  }
}
