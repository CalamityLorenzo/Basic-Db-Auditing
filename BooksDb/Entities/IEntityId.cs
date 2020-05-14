using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDb.Entities
{
    interface IEntityId<T> 
    {
        T Id { get; }
    }
}
