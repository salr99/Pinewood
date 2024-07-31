namespace Pinewood.CustomerApi.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException()
    {

    }

    public NotFoundException(string message) : base(message)
    {
    }
}
