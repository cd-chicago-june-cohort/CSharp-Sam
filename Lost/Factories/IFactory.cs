using Lost.Models;
using System.Collections.Generic;
namespace Lost.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}