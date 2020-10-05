using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
    }
}
