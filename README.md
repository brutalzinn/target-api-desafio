# Api Target Desafio

Essa é uma api que desenvolvi para o desafio da target pagamentos. Com o objetivo de executar um CRUD utilizando Microsoft SQL Server como banco de dados e uma api web com netcore 5.0. 

Requerimentos:

- NetCore runtime 5.0
- Microsoft SQL Server 2017
- Cliente para o Microsoft SQL Server(DBeaver,Microsoft SQL Server Management..)

# Instalação sem Docker:

1. Instale o Microsoft SQL Server. E configure seu login e senha.

2. Instale o cliente para o Microsoft SQL Server da sua preferência. 

3. Clone esse repositório na pasta da sua preferência.

4. Entre na pasta onde o repositório foi clonado, e entre na pasta api-target-desafio. Crie um arquivo appsettings.json e adicione essas linhas: 

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

5. Abra o seu cliente e se conecte ao Microsoft SQL Server como administrador.

6. Acesse a pasta scripts.

7. Copie tudo que está dentro do arquivo targetapi.sql e cole em uma query no seu cliente. Ou importe o arquivo targetapi.sql e execute.

8. Com a IDE da sua preferência, ou com o próprio visual studio, abra o arquivo api-target-desafio.sln e rode o projeto. Aguarde uma página da web abrir a documentação do Swagger.

9. Api está rodando. Você pode testar todas as rotas pelo próprio swagger.


# Instalação com Docker:

1. Clone esse repositório na pasta da sua preferência.

2. Abra a pasta do repositório clonado. Com o Docker instalado e o docker compose, execute o comando:
```
$ docker-compose up
```
3. Instale o cliente para o Microsoft SQL Server da sua preferência. 

4. Entre na pasta onde o repositório foi clonado, e entre na pasta api-target-desafio. Crie um arquivo appsettings.json e adicione essas linhas: 

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
    "app_target_api": "Server=localhost;Database=targetapidb;Integrated Security=false;User ID=sa;Password=iWadTqCC,!Aw"
  }
}
```

5. Abra o seu cliente e se conecte ao Microsoft SQL Server como administrador. Utilize o login ```sa``` e senha ``` iWadTqCC,!Aw ```

6. Acesse a pasta scripts.

7. Copie tudo que está dentro do arquivo targetapi.sql e cole em uma query no seu cliente. Ou importe o arquivo targetapi.sql e execute.

8. Com a IDE da sua preferência, ou com o próprio visual studio, abra o arquivo api-target-desafio.sln e rode o projeto. Aguarde uma página da web abrir a documentação do Swagger.

9. Api está rodando. Você pode testar todas as rotas pelo próprio swagger.


# Dados do Swagger com explicação para cada rota.

# Api Target Desafio
Api que desenvolvi para o desafio da target pagamentos. <3

## Version: v1

### /cliente

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

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Retorna um feedback sobre o cliente cadastrado. |
| 400 | Retorna um feedback sobre um cliente que não conseguiu efetuar o cadastro. |

### /cliente/{id}

#### GET
##### Sumário:

Listar clientes ou exibir apenas um único cliente.

##### Parâmetros

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | id is optional parameter | No | integer |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Retorna uma lista de clientes ou um cliente único. |
| 400 | Retorna um feedback sobre um cliente que não conseguiu efetuar o cadastro. |

#### PUT
##### Sumário:

Atualização de cliente.

##### Descrição:



##### Parâmetros

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Sim | integer |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |
| 400 | Retorna um feedback sobre um cliente que não conseguiu efetuar a atualização. |

### /cliente/date/{datestart}/{dateend}

#### GET
##### Sumário:

Listar clientes cadastrados por um período de datas

##### Parâmetros

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| datestart | path |  | Sim | dateTime |
| dateend | path |  | Sim | dateTime |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /financeiro/renda/{min}/{max}

#### GET
##### Sumário:

Retorna todos os clientes com base em um valor de RendaMensal mínimo ou ambos ao mesmo tempo.

##### Parâmetros

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| min | path |  | Sim | double |
| max | path | max is optional parameter | No | double |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /geo/uf

#### GET
##### Sumário:

Retorna todos os estados da API do IBGE.

##### Parâmetros

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /geo/cidade/{uf}

#### GET
##### Sumário:

Retorna todos os municípios de um estado específico da API do IBGE.

##### Parâmetros

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| uf | path | Id do estado | Sim | string |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /plano/detalhe

#### GET
##### Sumário:

Exibir quantidade de usuários compatíveis com o benefício de VIP.
Exibir quantidade de usuários com o benefício de VIP.
Exibir quantidade de usuários que são VIPS.
Exibir todos os ids de usuários para cada tipo de classificação.

##### Parâmetros

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | query |  | No | integer |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /plano/vip/{id}

#### GET
##### Sumário:

Exibir detalhe de um determinado plano vip.

##### Parâmetros

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | id is optional parameter | No | integer |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |

### /plano/manage

#### POST
##### Sumário:

Rota utilizada para o cliente confirmar o uso do plano VIP

##### Parâmetros

| Name | Localizado em | Descrição | Requerido | Esquema |
| ---- | ---------- | ----------- | -------- | ---- |
| api-key | header | API-KEY | Sim | string |

##### Respostas

| Código | Descrição |
| ---- | ----------- |
| 200 | Success |
