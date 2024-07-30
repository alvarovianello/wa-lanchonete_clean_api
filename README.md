
# Lanchonete - wa-lanchonete_clean_api

API desenvolvida em **clean architecture** para atender as funcionalidades de uma lanchonete proposta como projeto da Pós Tech FIAP.

## Youtube
Vídeo explicando a arquitetura do nosso projeto usando os princípios da Clean Architecture
https://youtu.be/rFIBIyeY8fY

## Stack utilizada

**Back-end:** .NET 8, C#

**Banco de dados:** PostgresSQL

## Gerenciador de Kluste
Docker Kubernetes

## Rodando Localmente

Instale o projeto wa-lanchonete_clean_api

1º Passo : Clone o projeto em sua máquina

2º Passo : Configurando projeto no Kubernetes - acesse o terminal PowerShell dentro da pasta Kubernetes, localizado dentro de onde o projeto foi clonado e execute os comandos abaixo para que seja configurado (docker precisa estar aberto)
- API
- Banco de dados
- Pgadmin : gerenciador do banco de dados

```bash
  kubectl apply -f wa-lanchonete-configmap.yaml
  kubectl apply -f wa-lanchonete-pod.yaml
  kubectl apply -f wa-lanchonete-deployment.yaml
  kubectl apply -f wa-lanchonete-pvc.yaml
  kubectl apply -f wa-lanchonete-hpa.yaml
  kubectl apply -f wa-lanchonete-service.yaml
  kubectl apply -f postgres-pvc.yaml
  kubectl apply -f postgres-deployment.yaml
  kubectl apply -f postgres-service.yaml
  kubectl apply -f pgadmin-deployment.yaml
  kubectl apply -f pgadmin-service.yaml
```

3º Passo: Configurando Pgadmin para visualização do banco de dados.
Acesse no navegador http://localhost:5050/browser/
- Usuário: admin@admin.com
- Senha: admin

Siga os passos do gif abaixo para configurar o Banco postgres no PGAdmin:
![App Screenshot](https://github.com/alvarovianello/wa-lanchonete_clean_api/blob/main/Config%20PGAdmin.gif?raw=true)

4º Passo: Configurando tabelas no banco de dados pelo PGAdmin.
Ainda conectado no Pgadmin após configurado o Banco;
 - Acesse a pasta WA.Persistence e copie o conteúdo do arquivo "DDL", no qual possui o script do banco de dados.

Siga os passos do gif abaixo para executar o script copiado do arquivo "DDL":
![App Screenshot](https://github.com/alvarovianello/wa-lanchonete_clean_api/blob/main/Configurar%20Tabelas.gif?raw=true)


## Documentação da API

API foi configurada na porta 3001: http://localhost:3001/swagger/index.html

Existe a própria documentação da API configurada no Swagger.
Abaixo encontra-se, em ordem demonstrada no Swagger ao ser executada a API, a descrição detalhada de todos os endpoints.
### **Category**
- **Cadastrar categoria**

```http
  POST /api/category
  BODY
  {
    "name": "string",
    "description": "string"
  }

```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome da categoria dos produtos ofertados|
| `description` | `string` | **Obrigatório**. Descrição da categoria|


- **Retornar todas as categorias**

```http
  GET /api/category

```
| Parâmetro   | 
| :---------- | 
| `No parameters`      |


- **Alterar categoria por ID**

```http
  PUT /api/category
  BODY
  {
    "id": 0,
    "name": "string",
    "description": "string"
  }

```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. ID da categoria a ser editada|
| `name` | `string` | **Obrigatório**. Nome da categoria dos produtos ofertados|
| `description` | `string` | **Obrigatório**. CDescrição da categoria|


- **Retornar categoria por ID**

```http
  GET /api/category/getcategorybyid/{id}
  BODY
  {
    "id": 1
  }

```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. ID da categoria a ser editada|


- **Retornar categoria por nome**

```http
  GET /api/category/getcategorybyname/{name}
  BODY
  {
    "name": "Bebida"
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome da categoria a ser editada|


### **Customer**
- **Cadastrar cliente**

```http
  POST /api/customer
  BODY
  {
    "name": "string",
    "cpf": "string",
    "email": "string"
  }

```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome do cliente|
| `cpf` | `string` | **Obrigatório**. CPF do cliente|
| `email` | `string` | **Obrigatório**. Email do cliente|


- **Retornar todos os clientes**

```http
  GET /api/customer
```
| Parâmetro   | 
| :---------- | 
| `No parameters`      | 


- **Alterar cadastro de cliente**

```http
  PUT /api/customer
  BODY
  {
    "name": "string",
    "cpf": "string",
    "email": "string"
  }

```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` | **Obrigatório**. Nome do cliente|
| `cpf` | `string` | **Obrigatório**. CPF do cliente|
| `email` | `string` | **Obrigatório**. Email do cliente|

- **Retornar cliente por CPF**

```http
  GET /api/customer/getbycpf/{cpf}
  BODY
  {
    "cpf": "string"
  }

```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `cpf` | `string` | **Obrigatório**. CPF do Customer a ser retornado|

- **Retornar cliente por ID**

```http
  GET /api/customer/getbyid/{id}
  BODY
  {
    "id": 1
  }

```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. ID do Customer a ser retornado|

- **Excluir cliente por ID**

```http
  DELETE /api/customer/{id}
  BODY
  {
    "id": 1
  }

```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. ID do Customer a ser excluido|


### **Order**

- **Retornar todos os pedidos**

```http
  GET /api/order
```

| Parâmetro   | 
| :---------- | 
| `No parameters`      |


- **Cadastrar pedido**

```http
  POST /api/order
  BODY
  {
    "customerId": 1,
    "orderItems": [
      {
        "productId": 1,
        "quantity": 1,
        "price": 10
      },
      {
        "productId": 2,
        "quantity": 2,
        "price": 10
      },
      {
        "productId": 3,
        "quantity": 1,
        "price": 3
      }
    ]
  }
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `customerId` | `int` | **Obrigatório**. Código 1 do Cliente Anônimo (Já cadastrado na tabela Customer) |
| `productId` | `int` | **Obrigatório**. Código do produto cadastrado em Produtos |
| `quantity` | `int` | **Obrigatório**. Quantidade de itens selecionados |
| `price` | `decimal` | **Obrigatório**. Valor total dos itens selecionado |


- **Retornar pedido por ID**

```http
  GET /api/order/{id}
  BODY
  {
    "id": 1
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Código do pedido|


- **Alterar pedido por ID**

```http
  PUT /api/order/{id}
  BODY
  {
    "id": 0,
    "customerId": 0,
    "orderItems": [
      {
        "productId": 0,
        "quantity": 0,
        "price": 0
      }
    ]
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Código do pedido|
| `customerId` | `int` | **Obrigatório**. Código 1 do Cliente Anônimo (Já cadastrado na tabela Customer) |
| `productId` | `int` | **Obrigatório**. Código do produto cadastrado em Produtos |
| `quantity` | `int` | **Obrigatório**. Quantidade de itens selecionados |
| `price` | `decimal` | **Obrigatório**. Valor total dos itens selecionado |


- **Excluir pedido por ID**

```http
  DELETE /api/order/{id}
  BODY
  {
    "id": 1
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. Código do pedido|

- **Retornar pedido por ordernumber**

```http
  GET /api/order/ordernumber
  BODY
  {
    "ordernumber": "string"
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `ordernumber` | `string` | **Obrigatório**. Número do pedido|


- **Retornar todos os pedido por status**

```http
  GET /api/order/getstatus
  BODY
  {
    "status": "string"
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `status` | `string` | **Obrigatório**. Status do pedido|


- **Atualizar status do pedido**

```http
  POST /api/order/updatestatus
  BODY
  {
    "id": int,
    "status": "string"
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `status` | `string` | **Obrigatório**. Status do pedido|


- **Retornar todos os produtos cadastrados**

```http
  GET /api/products
```

| Parâmetro   | 
| :---------- | 
| `No parameters`      |

- **Cadastrar produto**

```http
  POST /api/product
  BODY
  {
    "id": int,
    "categoryId": int,
    "name": "string",
    "description": "string",
    "price": double,
    "image": "string"
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. ID do produto|
| `categoryId` | `int` | **Obrigatório**. ID da categoria a qual o produto pertence|
| `name` | `string` | **Obrigatório**. Nome do produto|
| `description` | `string` | **Obrigatório**. Descrição do produto|
| `price` | `0` | **Obrigatório**. Preço do produto|
| `image` | `string` | **Obrigatório**. Imagem ilustrativa do produto|


- **Retornar produto por ID**

```http
  GET /api/product/{id}
  BODY
  {
    "id": int
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. ID do produto|

- **Alterar produto por ID**

```http
  PUT /api/product/{id}
  BODY
  {
    "id": int,
    "categoryId": int,
    "name": "string",
    "description": "string",
    "price": double,
    "image": "string"
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. ID do produto|
| `categoryId` | `int` | **Obrigatório**. ID da categoria a qual o produto pertence|
| `name` | `string` | **Obrigatório**. Nome do produto|
| `description` | `string` | **Obrigatório**. Descrição do produto|
| `price` | `0` | **Obrigatório**. Preço do produto|
| `image` | `string` | **Obrigatório**. Imagem ilustrativa do produto|


- **Excluir produto por ID**

```http
  DELETE /api/product/{id}
  BODY
  {
    "id": int
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `id` | `int` | **Obrigatório**. ID do produto|


- **Retornar produto por ID da categoria**

```http
  GET /api/product/{categoryId}
  BODY
  {
    "categoryId ": int
  }
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `categoryId ` | `int` | **Obrigatório**. ID da categoria do produto|

## Autores
- [@alvarovianello](https://github.com/alvarovianello)
- [@WilliaMarques7](https://github.com/https://github.com/WilliaMarques7)
