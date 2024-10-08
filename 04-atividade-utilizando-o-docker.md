Agora � sua vez de aplicar tudo que aprendeu! Vamos l�?!

Durante esta aula, aprendemos como utilizar o Docker para representar um banco de dados no cen�rio de testes de integra��o. Ent�o agora � sua vez de seguir as etapas e executar os passos que apresentamos durante esta aula. Voc� vai:

* Fazer o download do Docker Desktop;
* Instalar e utilizar a biblioteca Testcontainers.MSSQL;
* Aplicar a Interface IAsyncLifetime;
* Utilizar o Migrate para atualizar o docker com os dados de constru��o do banco.

Lembre-se que sempre que tiver d�vidas pode consultar a **Opini�o da Instrutora** logo abaixo. Boa pr�tica!


Ap�s fazer o download do Docker Desktop e instalar a biblioteca Testcontainers.MSSQL, � preciso adicionar o container e a imagem que ser� utilizada no `ContextoFixture`. O C�digo final ficar� assim:

```
public class ContextoFixture
{
    public JornadaMilhasContext Context { get; }
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();
    public ContextoFixture()
    {
        var options = new DbContextOptionsBuilder<JornadaMilhasContext>()
            .UseSqlServer(_msSqlContainer.GetConnectionString())
            .Options;

        Context = new JornadaMilhasContext(options);
    }
}
```
Feito isso, podemos passar para a parte de adicionar a comunica��o ass�ncrona implementando a interface IAsyncLifetime e adicionar o comando Migrate para configurar o banco. A classe `ContextoFixture` ficar� assim ao final desta etapa:

```
public class ContextoFixture: IAsyncLifetime
{
    public JornadaMilhasContext Context { get; private set; }
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
        var options = new DbContextOptionsBuilder<JornadaMilhasContext>()
            .UseSqlServer(_msSqlContainer.GetConnectionString())
            .Options;

        Context = new JornadaMilhasContext(options);
        Context.Database.Migrate();
    }

    public async Task DisposeAsync()
    {
        await _msSqlContainer.StopAsync();
    }
}
```

Voc� pode conferir o c�digo completo acessando o [reposit�rio no GitHub](https://github.com/alura-cursos/3659-JornadaMilhas-curso2/tree/aula03-video3.3).