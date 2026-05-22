!Faça tudo em C#
!mantenha o padrão do Projeto

Consulta para criar a Model:


alter view vw_analise as 
SELECT 
    nm_divida, ano, dia, 
    ISNULL([1], 0) AS Jan,
    ISNULL([2], 0) AS Fev,
    ISNULL([3], 0) AS Mar,
    ISNULL([4], 0) AS Abr,
    ISNULL([5], 0) AS Mai,
    ISNULL([6], 0) AS Jun,
    ISNULL([7], 0) AS Jul,
    ISNULL([8], 0) AS Ago,
    ISNULL([9], 0) AS 'Set',
    ISNULL([10], 0) AS 'Out',
    ISNULL([11], 0) AS Nov,
    ISNULL([12], 0) AS Dez
FROM (
    SELECT 
        nm_divida,
        day(dt_vencimento) AS dia,
        MONTH(dt_vencimento) AS mes,
        year(dt_vencimento) AS ano,
        nr_valor
    FROM dbo.tb_dividas
) AS SourceTable
PIVOT (
    SUM(nr_valor)
    FOR mes IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12])
) AS PivotTable

----------
Crie a Model para a view vw_analise, seguindo os padrões de projeto.
Crie o Repositorio para a Model criada, seguindo os padrões de projeto.
Faça um Metodo para buscar os dados da view vw_analise, seguindo os padrões de projeto.
Crie uma Camada chamada UseCases na pasta UseCases para implementar a consulta de análise, seguindo os padrões de projeto.
Crie um dto para retornar os dados da análise, seguindo os padrões de projeto.
Crie a Controller para a Model criada, 
Faça o endpoint para buscar os dados da análise, seguindo os padrões de projeto.


Ficará assim:
  A controller recebe a requisição, chama o UseCase correspondente, que por sua vez chama o Repository para acessar os dados no banco de dados.]
  O Repository utiliza o Contexto para realizar a operação de Consulta
  ?a view deve ser adicionada ao contexto do Entity Framework Core para ser utilizada no Repository.
  O UseCase de análise chama o Repository para buscar os dados da view vw_analise e retorna um DTO com os resultados.
  A Controller deve ser responsável por receber a requisição HTTP e retornar a resposta adequada.
