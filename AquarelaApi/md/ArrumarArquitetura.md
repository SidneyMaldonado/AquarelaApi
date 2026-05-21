!Faça tudo em C#
!respeite os padrões de projeto

#Conceito: Uma Controller deve receber o payload do usuario, quando isso acontece ela deve chamar o UseCase correspondente, que por sua vez chama o Repository para acessar os dados no banco de dados. O Repository utiliza o Contexto para realizar as operações de CRUD no banco de dados.
- A controller é responsável por receber as requisições HTTP, chamar os UseCases correspondentes e retornar as respostas adequadas.
- A controller não deve acessar o repositório diretamente, ela deve passar a responsabilidade para o UseCase, que é onde a lógica de negócio deve ser implementada.
- O UseCase é responsável por implementar a lógica de negócio, ele deve chamar o Repository para acessar os dados no banco de dados e realizar as operações necessárias.
- O Repository é responsável por realizar as operações de CRUD no banco de dados, ele deve utilizar o Contexto para acessar o banco de dados e realizar as operações necessárias.
- O Contexto é responsável por configurar a conexão com o banco de dados e fornecer os DbSet para as entidades do banco de dados, ele deve ser configurado para se conectar ao banco de dados utilizando a string de conexão definida no appsettings.json.
- Todos os payloads estão na pasta DTOs, e devem ser utilizados para receber os dados do usuário e retornar as respostas adequadas.
- O UseCase deve ser responsável por validar os dados recebidos do usuário, realizar as operações necessárias e retornar as respostas adequadas.
- Também no usecase que as dtos devem ser transformadas em entidades para serem utilizadas pelo Repository, e as entidades devem ser transformadas em dtos para serem retornadas para a controller.
- Para fazer a transformação deve ser utilizado o AutoMapper, que é uma biblioteca que facilita a transformação de objetos em C#.


Garantias:
   - A controller não deve acessar o repositório diretamente, ela deve passar a responsabilidade para o UseCase, que é onde a lógica de negócio deve ser implementada.
   - O UseCase é responsável por implementar a lógica de negócio, ele deve chamar o Repository para acessar os dados no banco de dados e realizar as operações necessárias.
