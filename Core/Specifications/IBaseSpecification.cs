using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface IBaseSpecification<T>
    {
        //generic methods and properties
        //the function takes a  generic type T which is the first parameter 
        //and returns boolean which is the second parameter

        Expression<Func<T, bool>> Criteria { get; }

        //returns a List of objecta
        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }

        int Skip { get; }

        bool IsPagingEnabled { get; }
    }
}
