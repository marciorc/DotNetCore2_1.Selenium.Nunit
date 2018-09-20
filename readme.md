DotNetCore 2.1 + Selenium + Nunit
--------------------------------------------------

## Instalacão

Clone o repositório:

```git clone git@github.com:marciorc/DotNetCore2_1.Selenium.Nunit.git```

## Configuração

Extraia o arquivo **`Selenium.rar`** e mova para a pasta **`C:/`**. Nele contém todos os webdrivers necessários parar execução dos testes.
 
Acesse o arquivo **`appsettings.json`** para setar as configurações do projeto

- Configure a **Url**
- Configure o **DefaultTimeout**

### Configuração IE Webdriver

O Webdriver do IE possui algumas peculiaridades para que a execução funcione.

É necessário alterar as seguintes configurações antes de executar os teste com o **IE WebDriver**:
- Manter o Zoom em 100%
- Em `Configurações > Opções de Internet > Segurança` é necessário **desabilitar** a opção `Habilitar Modo Protegido` para todas opções (Internet, Intranet Local, Sites Confiáveis e Sites Restritos).

## PageObjects

Para a listagem dos **PageObjects** foram definidos alguns padrões na nomenclatura dos elementos.
Segue o padrão definido:

- **Textos**, devem ser declarados com o padrão:
  - `Txt`NomeElemento
- **Links**, devem ser declarados com o padrão:
  - `Lnk`NomeElemento
- **Radio Buttons** devem ser declarados com o padrão:
  - `Rdb`NomeElemento
- **CheckBox** devem ser declarados com o padrão:
  - `Ckb`NomeElemento
- **TextField** e **Inputs** devem ser declarados com o padrão:
  - `Inp`NomeElemento
- **Buttons** devem ser declarados com o padrão:
  - `Btn`NomeElemento
- **Images** devem ser declarados com o padrão:
  - `Img`NomeElemento
- **Select** devem ser declarados com o padrão:
  - `Sel`NomeElemento