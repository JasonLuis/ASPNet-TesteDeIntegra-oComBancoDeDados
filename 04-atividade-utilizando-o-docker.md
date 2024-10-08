Agora é sua vez de aplicar tudo que aprendeu! Vamos lá?!

Durante esta aula, aprendemos como utilizar o Docker para representar um banco de dados no cenário de testes de integração. Então agora é sua vez de seguir as etapas e executar os passos que apresentamos durante esta aula. Você vai:

* Fazer o download do Docker Desktop;
* Instalar e utilizar a biblioteca Testcontainers.MSSQL;
* Aplicar a Interface IAsyncLifetime;
* Utilizar o Migrate para atualizar o docker com os dados de construção do banco.

Lembre-se que sempre que tiver dúvidas pode consultar a **Opinião da Instrutora** logo abaixo. Boa prática!


Após fazer o download do Docker Desktop e instalar a biblioteca Testcontainers.MSSQL, é preciso adicionar o container e a imagem que será utilizada no `ContextoFixture`. O Código final ficará assim:

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
Feito isso, podemos passar para a parte de adicionar a comunicação assíncrona implementando a interface IAsyncLifetime e adicionar o comando Migrate para configurar o banco. A classe `ContextoFixture` ficará assim ao final desta etapa:

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

Você pode conferir o código completo acessando o [repositório no GitHub](https://github.com/alura-cursos/3659-JornadaMilhas-curso2/tree/aula03-video3.3).