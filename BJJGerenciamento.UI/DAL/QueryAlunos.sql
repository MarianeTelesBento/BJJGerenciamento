CREATE TABLE TBAlunos (
	IdAlunos int IDENTITY(1,1),
	IdTurma int NOT NULL,
	Matricula varchar(5) NOT NULL,
	Nome varchar(25) NOT NULL,
	Sobrenome varchar(50) NOT NULL,
	EstadoMatricula bit NOT NULL,
	Telefone varchar(15) NOT NULL,
	Email varchar(50) NOT NULL,
	Rg varchar(10) NOT NULL,
	Cpf varchar(11) NOT NULL,
	DataNascimento date NOT NULL,
	CEP varchar(8) NULL,
	Endereco varchar(100) NULL,
	Bairro varchar(50) NULL,
	Numero varchar(10) NOT NULL
)

SELECT * FROM TBAlunos;

DROP TABLE Alunos;

insert into TBAlunos(IDTurma, Matricula, Nome, Sobrenome, EstadoMatricula, Telefone, Email, Rg, Cpf, DataNascimento, CEP, Endereco, Bairro, Numero) values(2, 'fds', '@d2', '@d3', True, '@d4', '@d5', '@d6', '@d7', '@d8', '@d9', '@d10', '@d11', '@d12'); SELECT SCOPE_IDENTITY();