using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSAA.Domain.Entities.GL_Income
{
    public class GL_IncomeModel
    {
        public virtual int Income_Booking_Resp_GL_ID { get; set; }
        public virtual string? GL_Code { get; set; }
        public virtual string? GL_Description { get; set; }
        public virtual decimal Amount { get; set; }
    }
}
