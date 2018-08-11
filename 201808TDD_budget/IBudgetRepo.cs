using System.Collections.Generic;

namespace _201808TDD_budget
{
    public interface IBudgetRepo
    {
        List<Budget> GetAll();
    }
}