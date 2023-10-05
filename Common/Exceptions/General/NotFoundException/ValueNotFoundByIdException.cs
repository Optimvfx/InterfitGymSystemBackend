namespace Common.Exceptions.General.NotFoundException
{
    public class ValueNotFoundByIdException : ValueNotFoundedException
    {
        public readonly string NotFoundModel;
        public readonly Guid? Id;

        public ValueNotFoundByIdException(string notFoundModel, Guid id)
        {
            NotFoundModel = notFoundModel;
            Id = id;
        }

        public ValueNotFoundByIdException(string notFoundModel)
        {
            NotFoundModel = notFoundModel;
        }
        
        public ValueNotFoundByIdException(Type notFoundModel, Guid id)
        {
            NotFoundModel = notFoundModel.Name;
            Id = id;
        }

        public ValueNotFoundByIdException(Type notFoundModel)
        {
            NotFoundModel = notFoundModel.Name;
        }

        public override object GetValue() => NotFoundModel;

        public override object? GetKey()
        {
            if (Id == null)
                return null;

            return Id.ToString();
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
