Para reverter dados da base de testes em cenários mais complexos, temos algumas bibliotecas que podem auxiliar. A biblioteca Respawn é uma delas, pois permite que você defina um estado inicial conhecido para o banco de dados antes de cada teste e depois limpe quaisquer alterações feitas durante o testes, deixando o banco de dados em seu estado inicial.

Basicamente, ela trabalha com três etapas: primeiro salva o estado atual do banco em um snapshot, depois os testes são executados e por fim a biblioteca restaura o banco para o estado que estava antes dos testes serem executados.

Para conhecer mais sobre a biblioteca Respawn e verificar exemplos de utilização, você pode [acessar esse artigo](https://medium.com/@kova98/easy-test-database-reset-in-net-with-respawn-d5a59f995e9d).