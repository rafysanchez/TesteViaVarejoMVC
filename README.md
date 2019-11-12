# TesteViaVarejoMVC
Teste Via Varejo MVC
uma tela de Cadastro onde:
 Cadastra um usuário novo na aplicação
Premissas:
 A tela deverá fazer todas as chamadas em ajax
 A tela somente envia os parâmetros para api
 Deverá ser desenvolvida com MVC
 Não deve permitir sql injection
 Não deve permitir buffer overflow
 E demais requisitos de segurança
Implementação do back
Log-in
1) Recebe o usuário e senha como parâmetro;
2) Consulta o usuário no banco retornando o sha1 e salt;
3) Concatena a senha do usuário com o salt sem espaços [Senha][Salt];
4) Converte a [Senha][Salt] para sha1;
5) Compara o resultado da conversão ao sha1 armazenado no banco;
6) Se os dados forem iguais retorna usuário autenticado;
Cadastrar
1) Insere os dados do usuário;
Atualizar
1) Atualiza os dados do usuário;
2) Exclui dados do usuário pedindo confirmação;
Premissas:
 Utilizar o banco mdf
 O salt deve ser o seu nome
 Utilizar os conceitos web api rest
 Caso conheça algum padrão pode utilizar
..
