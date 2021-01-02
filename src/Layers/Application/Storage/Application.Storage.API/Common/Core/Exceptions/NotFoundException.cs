using System;

namespace Application.Storage.API.Common.Core.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}