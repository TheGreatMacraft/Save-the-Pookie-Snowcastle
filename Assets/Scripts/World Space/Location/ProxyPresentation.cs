using System;

public sealed class ProxyPresentation : Presentation
{
    private readonly Action presentation;

    public ProxyPresentation(Func<Presentation> presentation)
    {
        this.presentation = () => presentation().Present();
    }

    public void Present() => presentation();
}