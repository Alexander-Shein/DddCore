namespace DddCore.Contracts.SL.Services.Application.RestFull
{
    public interface IViewModel
    {
        Links Links { get; set; }
        Extends Extends { get; set; }
    }
}