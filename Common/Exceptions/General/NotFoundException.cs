using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions.General
{
    public class NotFoundException : Exception
    {
        public readonly string NotFoundModel;
        public readonly Guid? Id;

        public NotFoundException(string notFoundModel, Guid id)
        {
            NotFoundModel = notFoundModel;
            Id = id;
        }

        public NotFoundException(string notFoundModel)
        {
            NotFoundModel = notFoundModel;
        }
        
        public NotFoundException(Type notFoundModel, Guid id)
        {
            NotFoundModel = notFoundModel.Name;
            Id = id;
        }

        public NotFoundException(Type notFoundModel)
        {
            NotFoundModel = notFoundModel.Name;
        }

        public override string Message => GetMessage();

        private string GetMessage()
        {
            if (Id == null)
                return $"{NotFoundModel} is not found.";

            return $"{NotFoundModel} is not found by id({Id}).";
        }
    }
}
