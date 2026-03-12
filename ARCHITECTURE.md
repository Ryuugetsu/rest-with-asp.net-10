# Arquitetura do projeto

## Core
Guarda:
- entidades
- interfaces
- regras
- serviços

## Infrastructure
Guarda:
- DbContext
- repositórios
- persistência

## WebApi
Guarda:
- controllers
- configs
- request/response
- DTOs

## Fluxo da requisição
Controller -> Service -> Repository -> DbContext -> Banco