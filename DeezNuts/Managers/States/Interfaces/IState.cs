using DeezNuts.Data.Models;
using DeezNuts.Dtos;

namespace DeezNuts.Managers.States.Interfaces
{
    public interface IState
    {
        Customer DoAction(StateActionDto dto);
    }
}
