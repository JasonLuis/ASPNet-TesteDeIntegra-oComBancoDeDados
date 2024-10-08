using JornadaMilhas.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integracao;

[Collection(nameof(ContextoCollection))]
public class OfertaViagemDalRecuperarTodoas: IDisposable
{
    private readonly JornadaMilhasContext context;
    private readonly ContextoFixture fixture;

    public OfertaViagemDalRecuperarTodoas(ITestOutputHelper output, ContextoFixture fixture)
    {
        context = fixture.Context;
        this.fixture = fixture;
        output.WriteLine(context.GetHashCode().ToString());
    }

    public void Dispose()
    {
        fixture.LimpaDadosDoBanco();
    }

    [Fact]
    public void RecuperarOfertasNoBanco()
    {
        //arrange 
        fixture.CriaDadosFake();
        var dal = new OfertaViagemDAL(context);

        //act
        var ofertas = dal.RecuperarTodas();

        //assert
        Assert.NotNull(ofertas);
        Assert.NotEmpty(ofertas);
    }
}
