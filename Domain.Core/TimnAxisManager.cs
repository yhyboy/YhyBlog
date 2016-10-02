using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interface;
using Domain.Model.Blog;

namespace Domain.Core
{
    [Export(typeof(ITimnAxisManager))]
    public class TimnAxisManager : BaseManager<TimnAxis, int>,ITimnAxisManager
    {
    }
}
