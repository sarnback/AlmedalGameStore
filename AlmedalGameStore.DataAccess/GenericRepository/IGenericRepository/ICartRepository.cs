using AlmedalGameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        int IncrementCount(Cart shoppingCart, int count);
        int DecrementCount(Cart shoppingCart, int count);


    }
}
