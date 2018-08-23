using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
   public static class TargetType
    {
       public const string Target = "_blank;_self;_parent;_top";
       //_blank	Opens the linked document in a new window or tab
       //_self	Opens the linked document in the same frame as it was clicked (this is default)
       //_parent	Opens the linked document in the parent frame
       //_top	Opens the linked document in the full body of the window
    }
}
