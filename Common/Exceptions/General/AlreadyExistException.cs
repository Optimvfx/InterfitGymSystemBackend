using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions.General
{
    public class AlreadyExistException : Exception
    {
        public readonly string AlreadyExistsModel;

        public AlreadyExistException(string alreadyExistsModel)
        {
            AlreadyExistsModel = alreadyExistsModel;
        }

        public AlreadyExistException(Type alreadyExistsModel)
        {
            AlreadyExistsModel = alreadyExistsModel.Name;
        }

        public override string Message => $"{AlreadyExistsModel} is already exists.";
    }
}
