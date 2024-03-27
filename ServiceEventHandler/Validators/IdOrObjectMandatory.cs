using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEventHandler.Validators
{
    public static class IdOrObjectMandatory
    {
        public static bool Validate(int? id, object? objectCommand)
        {
            if (id == 0 && objectCommand == null)
            {
                return false;
            }
            return true;
        }
    }
}
