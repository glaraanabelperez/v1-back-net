using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEventHandler.Validators
{
    public static class Validations
    {
        public static bool IdOrObjectMandatory(int? id = null, object? objectCommand = null)
        {
            if (id == null && objectCommand == null ||  id != null && objectCommand != null)
            {
                return false;
            }
            return true;
        }
    }
}
