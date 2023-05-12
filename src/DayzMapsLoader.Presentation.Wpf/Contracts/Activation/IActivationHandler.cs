namespace DayzMapsLoader.Presentation.Wpf.Contracts.Activation;

public interface IActivationHandler
{
    bool CanHandle();

    Task HandleAsync();
}
