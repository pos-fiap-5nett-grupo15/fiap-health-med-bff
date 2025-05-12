
# Hackaton - Fiap.Health.Med.Bff

Projeto criado pelo **Grupo 15** do curso de **Arquitetura de Sistemas .NET com Azure** da Fiap para atender o Hackaton.

> O Bff tem como principal função receber e direcionar as requisições no ambiente para os respectivos serviços, recebendo as requisições e retornando as devídas respostas de acordo com o processamento de cada serviço chamado.


## Autores

- Grupo 15

|Integrantes|
|--|
| Caio Vinícius Moura Santos Maia |
| Evandro Prates Silva |
| Guilherme Castro Batista Pereira |
| Luis Gustavo Gonçalves Reimberg |


## Stack utilizada

|Tecnologia utilizada|
|--|
|.Net 8|
|Docker|
|FluentValidation|
|Jwt|
|BCypt|
|Swagger|
|XUnit|
|Grafana K6|


## Funcionalidades

- Receber as requisições de ambos os contextos: Usuários(médico e paciente) e Agendamentos
- Directionar requisições as respectivos serviços: Usuários e/ou Agendamentos
- Autenticação de usuários
- Limitação de acesso para determinados endpoits, conforme permissão de cada perfil


## Build do projeto

``` shell
    docker build -f ./infrastructure/docker/api/Dockerfile -t schedule-app .
```

Ou se estiver na pasta de infra

``` shell
    docker build -f ../fiap-health-med-bff/infrastructure/docker/api/Dockerfile -t bff-app ../fiap-health-med-bff/
```