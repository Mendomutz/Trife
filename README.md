# Avalonia

## Configurações

### Configuração do banco de dados

- Para configurar a conexão com o banco de dados, vá até o arquivo `appsettings.json` e altere a string de conexão.

### Publicação

- Para publicar o projeto `Trife`, execute o comando:
  - >**dotnet publish Trife.Desktop\Trife.Desktop.csproj -c Release -r win-x64 --self-contained -p:PublishSingleFile=true -o C:\Projetos\Avalonia\Trife\Output**

- Obs: Troque o caminho do parâmetro `-o` para o caminho desejado.

### Criando o instalador

- Para criar um instalador, baixe o [`Inno`](https://jrsoftware.org/isdl.php#stable)
  - Abra o arquivo [TrifeInstaller.iss](Trife/TrifeInstaller.iss) e clique em `Compile`, ele irá gerar uma pasta com o executável.

### Local Storage

- O sistema utiliza um arquivo de armazenamento local para quando o banco de dados estiver indisponível.
- Para acessar o arquivo de armazenamento local, siga os passos abaixo.
  - Pressione `Win + R`
  - Digite `%APPDATA%\..\Local\Trife`

#### Criando o arquivo de depedências

- Para exportar os pacotes da solução, vá até a raiz da solução e execute o comando:
  - >**dotnet list package > packages.txt**
  - Ele irá criar um arquivo `.txt` com todos os pacotes instalados em todos os projetos da solução
