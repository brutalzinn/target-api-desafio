# Api Target Desafio - Em construção

Essa é uma api que desenvolvi para o desafio da target pagamentos. Com o objetivo de executar um CRUD utilizando Microsoft SQL Server como banco de dados e uma api web com netcore 5.0. 

Requerimentos:

- NetCore runtime 5.0
- Microsoft SQL Server

# Instalação:

Clone esse repositório, entre na pasta api-target-desafio, crie um arquivo appsettings.json e adicione essas linhas: 

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "app_target_api": "Server=localhost;Database=targetapidb;Integrated Security=false;User ID=<seulogin>;Password=<senhadoseubanco>"
  }
}

```

# Api Target Desafio
Api que desenvolvi para o desafio da target pagamentos. <3

## Version: v1

**Contact information:**  
Roberto Caneiro Paes  
https://github.com/brutalzinn  

### /user

#### POST
##### Sumário:

Cadastro de cliente.

##### Descrição:

Exemplo de resposta(200) - cliente com renda superior ou igual a 6000 reais:
            
    POST /user
    {
       "oferecerPlanoVip": true,
       "cadastrado": "true",
       "error": false
    }
    
   Cliente com renda inferior a 6000 reais
            
    POST /user
    {
       "oferecerPlanoVip": false,
       "cadastrado": "true",
       "error": false
    }
            
    Cliente não conseguiu efetuar o cadastro.
Exemplo de resposta(400) :
            
    POST /user
    {
       "oferecerPlanoVip": false,
       "cadastrado": "false",
       "error": true
    }

##### Parâmetros

| Name | Located in | Descrição | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| api-key | header | API-KEY | Yes | string |

##### Responses

| Código | Descrição |
| ---- | ----------- |
| 200 | Retorna um feedback sobre o cliente cadastrado. |
| 400 | Retorna um feedback sobre um cliente que não conseguiu efetuar o cadastro. |

### /user/{id}

#### GET
##### Sumário:

Listar clientes ou exibir apenas um único cliente.

##### Descrição:

Exemplo de resposta(200) - cliente renda superior ou igual a 6000 reais:

    GET /user/1
    
    {
    "id": 1,
    "nomeCompleto": "Fernando Paes",
    "cpf": "177.811.940-98",
    "dataNascimento": "2019-01-06T02:00:00Z",
    "dateCadastro": "0001-01-01T02:00:00Z",
    "dateModificado": "0001-01-01T02:00:00Z",
    "endereco": {
        "id": 1,
        "logradouro": "ARSO 23 Alameda 4",
        "bairro": "Plano Diretor Sul",
        "cidade": "Palmas",
        "uf": "TO",
        "cep": "770153-14",
        "complemento": "Complemento"
     },
     "financeiro": {
         "id": 1,
        "rendaMensal": 350.50
      }
      }

    GET /user
    
    [{
    "id": 1,
    "nomeCompleto": "Fernando Paes",
    "cpf": "177.811.940-98",
    "dataNascimento": "2019-01-06T02:00:00Z",
    "dateCadastro": "0001-01-01T02:00:00Z",
    "dateModificado": "0001-01-01T02:00:00Z",
    "endereco": {
        "id": 1,
        "logradouro": "ARSO 23 Alameda 4",
        "bairro": "Plano Diretor Sul",
        "cidade": "Palmas",
        "uf": "TO",
        "cep": "770153-14",
        "complemento": "Complemento"
     },
     "financeiro": {
         "id": 1,
        "rendaMensal": 350.50
      }
      }]

##### Parâmetros

| Name | Located in | Descrição | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |
| api-key | header | API-KEY | Yes | string |

##### Responses

| Código | Descrição |
| ---- | ----------- |
| 200 | Retorna uma lista de clientes ou um cliente único. |
| 400 | Retorna um feedback sobre um cliente que não conseguiu efetuar o cadastro. |

#### PUT
##### Sumário:

Atualização de cliente.

##### Descrição:



##### Parâmetros

| Name | Located in | Descrição | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |
| api-key | header | API-KEY | Yes | string |

##### Responses

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |
| 400 | Retorna um feedback sobre um cliente que não conseguiu efetuar a atualização. |

### /user/date/{datestart}/{dateend}

#### GET
##### Sumário:

Listar clientes cadastrados por um período de datas

##### Parâmetros

| Name | Located in | Descrição | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| datestart | path |  | Yes | dateTime |
| dateend | path |  | Yes | dateTime |
| api-key | header | API-KEY | Yes | string |

##### Responses

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /financeiro/renda/{min}/{max}

#### GET
##### Sumário:

Retorna todos os clientes com base em um valor de RendaMensal mínimo ou ambos ao mesmo tempo.

##### Parâmetros

| Name | Located in | Descrição | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| min | path |  | Yes | double |
| max | path |  | Yes | double |
| api-key | header | API-KEY | Yes | string |

##### Responses

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /financeiro/vips/count

#### GET
##### Parâmetros

| Name | Located in | Descrição | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| api-key | header | API-KEY | Yes | string |

##### Responses

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /geo/uf

#### GET
##### Sumário:

Retorna todos os estados da API do IBGE.

##### Parâmetros

| Name | Located in | Descrição | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| api-key | header | API-KEY | Yes | string |

##### Responses

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /geo/cidade/{uf}

#### GET
##### Sumário:

Retorna todos os municípios de um estado específico da API do IBGE.

##### Parâmetros

| Name | Located in | Descrição | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| uf | path | Id do estado | Yes | string |
| api-key | header | API-KEY | Yes | string |

##### Responses

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /plan/detail/vip

#### GET
##### Sumário:

Exibir quantidade de usuários compatíveis com o benefício de VIP.

##### Parâmetros

| Name | Located in | Descrição | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| api-key | header | API-KEY | Yes | string |

##### Responses

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |
