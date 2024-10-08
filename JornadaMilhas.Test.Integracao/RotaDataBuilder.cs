using Bogus;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test.Integracao;

internal class RotaDataBuilder: Faker<Rota>
{
    public string? Origem { get; set; }
    public string? Destino { get; set; }

    public RotaDataBuilder()
    {
        CustomInstantiator(f => {
            string origem = Origem ?? "Alagoas";
            string destino = Destino ?? "São Paulo";
            return new Rota(origem, destino);
        });
    }

    public Rota Build() => Generate();
}
