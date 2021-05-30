# blazor-exemplo-states-components
Blazor c# exemplo de states e components

Injeção de dependências no Main do Program

```c#
  public static async Task Main(string[] args)
  {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("app");

      builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
      builder.Services.AddScoped<IClienteService, ClienteService>();

      await builder.Build().RunAsync();
  }
```

Página para mostrar as etapas

```c#
<button class="btn btn-secondary" @onclick="MostrarEtapas">Mostrar Etapas</button>

@if (etapasExibidas)
{
    <Etapas></Etapas>
}

@code {

    private bool etapasExibidas = false;

    private void MostrarEtapas()
    {
        etapasExibidas = !etapasExibidas;
    }

}
```

Component de Etapas

```c#
@using BlazorWebApp01.Services.Contracts
@using BlazorWebApp01.Services.ViewModels

@inject IClienteService clienteService

<h3>Componente de Etapas</h3>

@dataExibicao

<Etapa1 Visible="etapa == 1" DataExibicaoPai="dataExibicao" Clientes="clientes"></Etapa1>
<Etapa2 Visible="etapa == 2" DataExibicaoPai="dataExibicao"></Etapa2>
<Etapa3 Visible="etapa == 3" DataExibicaoPai="dataExibicao"></Etapa3>

<button class="btn btn-secondary" @onclick="Voltar" disabled="@disableVoltar">Voltar</button>
<button class="btn btn-primary" @onclick="Avancar" disabled="@disableAvancar">Avançar</button>

@if (carregando)
{
    <p>
        Carregando ....
    </p>
}


@code {
    private int etapa;

    private DateTime dataExibicao;

    private bool disableVoltar;
    private bool disableAvancar;

    private bool carregando;

    private List<Cliente> clientes;

    protected override async Task OnInitializedAsync()
    {
        dataExibicao = DateTime.Now;
        etapa = 1;
        carregando = true;
        DisableButtons();

        await Task.Run(() =>
        {
            clientes = clienteService.GetAll().Result;
            carregando = false;
        });

    }

    private void Voltar()
    {
        if (etapa > 1)
            etapa--;

        DisableButtons();
    }

    private void Avancar()
    {
        if (etapa < 3)
            etapa++;

        DisableButtons();
    }

    private void DisableButtons()
    {
        disableVoltar = etapa == 1;
        disableAvancar = etapa == 3;
    }

}
```

Component de Etapa 1

```c#
@using BlazorWebApp01.Services.ViewModels

@if (Visible)
{
    <h3>Etapa 1 - Data de exibição do Filho @data</h3>
    <h3>Data Exibição do Pai @DataExibicaoPai</h3>

    @if (Clientes != null)
    {
        @foreach (var cliente in Clientes)
        {
            <p>@cliente.NomeCliente</p>
        }
    }
}
@code {

    [Parameter]
    public bool Visible { get; set; }

    [Parameter]
    public DateTime DataExibicaoPai { get; set; }

    [Parameter]
    public List<Cliente> Clientes { get; set; }

    private string data = "";

    protected override void OnInitialized()
    {
        data = $"Chamou em: {DateTime.Now.ToString()}";
    }
}
```
