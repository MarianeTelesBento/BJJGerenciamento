SELECT * FROM TbResponsaveis;

CREATE TABLE [dbo].[TBAlunos] (
    [IdAluno]        INT           IDENTITY (1, 1) NOT NULL,
	[IdPlano]		INT NULL,
	[IdResponsavel] INT NULL,
    [Nome]            VARCHAR (100) NOT NULL,
    [Sobrenome]       VARCHAR (100) NOT NULL,
    [Telefone]        VARCHAR (20)  NULL,
    [Email]           VARCHAR (255) NULL,
    [Rg]              VARCHAR (20)  NOT NULL,
    [Cpf]             VARCHAR (14)  NOT NULL,
    [DataNascimento]  DATE          NOT NULL,
    [CEP]             VARCHAR (10)  NULL,
    [Endereco]        VARCHAR (255) NOT NULL,
    [Bairro]          VARCHAR (100) NOT NULL,
    [Cidade]          VARCHAR (100) NOT NULL,
    [Estado]          VARCHAR (100) NOT NULL,
    [NumeroCasa]          VARCHAR (10)  NOT NULL,
	[CarteiraFPJJ]	VARCHAR(100)	NULL,
	[Complemento] VARCHAR(100) NULL,
    PRIMARY KEY CLUSTERED ([IdAluno] ASC),
    UNIQUE NONCLUSTERED ([Cpf] ASC)
);


---Atual---


CREATE TABLE TbPlanos (
    IdPlano INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(25),
    QtsDias INT,
    Mensalidade DECIMAL(10,2)
);

CREATE TABLE TbResponsaveis (
    IdResponsavel INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(25) NULL,
    Sobrenome VARCHAR(50) NULL,
    CPF VARCHAR(14) UNIQUE NULL,
    RG VARCHAR(20) NULL,
    Telefone VARCHAR(20) NULL,
	email varchar (100) NULL,
    Bairro VARCHAR(50) NULL,
    CEP VARCHAR(10),
    Cidade VARCHAR(50) NULL,
    Rua VARCHAR(50),
	Estado varchar(20),
	DataDeNascimento DATE,
    NumeroCasa INT NULL
);

CREATE TABLE TbAlunos (
    IdAlunos INT PRIMARY KEY IDENTITY(1,1),
    IdPlano INT NULL,
    IdResponsavel INT NULL,
    Nome VARCHAR(25) NOT NULL,
    Sobrenome VARCHAR(50) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Email VARCHAR(50) NULL,
    DataNascimento DATE,
    CPF VARCHAR(14) UNIQUE NOT NULL,
    RG VARCHAR(20) NOT NULL,
    Estado VARCHAR(20) NOT NULL,
    Bairro VARCHAR(50) NOT NULL,
    Cidade VARCHAR(50) NOT NULL,
    Rua VARCHAR(50) NOT NULL,
    NumeroCasa INT NOT NULL,
    Complemento VARCHAR(50) NULL,
    CEP VARCHAR(10) NULL,
    CarteiraFPJJ VARCHAR(50) NULL,
    FOREIGN KEY (IdPlano) REFERENCES TbPlanos(IdPlano),
    FOREIGN KEY (IdResponsavel) REFERENCES TbResponsaveis(IdResponsavel)
);

DROP TABLE TbAlunos;

CREATE TABLE TbProfessor (
    IdProfessor INT PRIMARY KEY,
    Nome VARCHAR(50),
    CPF VARCHAR(14) UNIQUE,
    CEP VARCHAR(10),
    Rua VARCHAR(50),
    Bairro VARCHAR(50),
    CarteiraFPJJ VARCHAR(20),
    CarteiraCBJJ VARCHAR(20)
);

CREATE TABLE TbCalendario (
    IdCalendario INT PRIMARY KEY,
    Data DATE,
    Hora TIME
);

CREATE TABLE TbPresencas (
    IdPresenca INT PRIMARY KEY,
    IdData INT,
    IdAluno INT,
    IdProfessor INT,
    StatusPresenca BIT, -- 1 para presente, 0 para ausente
    FOREIGN KEY (IdData) REFERENCES TbCalendario(IdCalendario),
    FOREIGN KEY (IdAluno) REFERENCES TbAlunos(IdAlunos),
    FOREIGN KEY (IdProfessor) REFERENCES TbProfessor(IdProfessor)
);



-- Inserindo dados na tabela TbPlanos
INSERT INTO TbPlanos (Nome, QtsDias, Mensalidade) VALUES
('Básico', 30, 100.00),
('Intermediário', 60, 180.00),
('Avançado', 90, 250.00),
('Premium', 120, 300.00),
('Profissional', 150, 400.00),
('Elite', 180, 500.00),
('Kids', 30, 80.00),
('Juvenil', 60, 150.00),
('Master', 120, 350.00),
('Especial', 365, 1000.00);

-- Inserindo dados na tabela TbResponsavel
INSERT INTO TbResponsaveis (Nome, Sobrenome, CPF, RG, Telefone, Email, Bairro, CEP, Cidade, Rua, Estado, DataDeNascimento, NumeroCasa) VALUES
('Carlos', 'Silva', '123.456.789-00', 'MG-123456', '31999998888', 'carlos@email.com', 'Centro', '30123-456', 'Belo Horizonte', 'Rua A', 'MG', '1980-05-15', 100),
('Fernanda', 'Souza', '987.654.321-00', 'SP-654321', '11988887777', 'fernanda@email.com', 'Jardins', '01456-789', 'São Paulo', 'Avenida B', 'SP', '1985-10-20', 200),
('Ricardo', 'Lima', '456.789.123-00', 'RJ-789123', '21977776666', 'ricardo@email.com', 'Copacabana', '22040-010', 'Rio de Janeiro', 'Rua C', 'RJ', '1990-02-25', 50),
('Juliana', 'Oliveira', '321.654.987-00', 'PR-321654', '41966665555', 'juliana@email.com', 'Centro', '80010-000', 'Curitiba', 'Rua D', 'PR', '1983-07-30', 75),
('Marcos', 'Ferreira', '111.222.333-44', 'RS-111222', '51955554444', 'marcos@email.com', 'Moinhos de Vento', '90450-000', 'Porto Alegre', 'Rua E', 'RS', '1975-12-10', 30),
('Paula', 'Mendes', '555.666.777-88', 'SC-555666', '48944443333', 'paula@email.com', 'Centro', '88010-400', 'Florianópolis', 'Rua F', 'SC', '1992-09-05', 90),
('Gabriel', 'Santos', '999.888.777-66', 'PE-999888', '81933332222', 'gabriel@email.com', 'Boa Viagem', '51020-010', 'Recife', 'Rua G', 'PE', '1988-11-15', 45),
('Amanda', 'Carvalho', '777.666.555-44', 'BA-777666', '71922221111', 'amanda@email.com', 'Pituba', '41810-000', 'Salvador', 'Rua H', 'BA', '1995-04-28', 55),
('Thiago', 'Ribeiro', '222.333.444-55', 'CE-222333', '85911110000', 'thiago@email.com', 'Meireles', '60165-081', 'Fortaleza', 'Rua I', 'CE', '1981-08-18', 70),
('Luciana', 'Barbosa', '333.444.555-66', 'PA-333444', '91900001111', 'luciana@email.com', 'Centro', '66010-000', 'Belém', 'Rua J', 'PA', '1977-06-22', 35);

-- Inserindo dados na tabela TbAlunos
INSERT INTO TbAlunos (IdPlano, IdResponsavel, Nome, Sobrenome, Telefone, Email, DataNascimento, CPF, RG, Estado, Bairro, Cidade, Rua, NumeroCasa, Complemento, CEP, CarteiraFPJJ) VALUES
(1, 1, 'Lucas', 'Silva', '31999990000', 'lucas@email.com', '2005-03-10', '123.456.789-11', 'MG-123457', 'MG', 'Centro', 'Belo Horizonte', 'Rua A', 100, NULL, '30123-456', 'FPJJ12345'),
(2, 2, 'Beatriz', 'Souza', '11988889999', 'beatriz@email.com', '2008-07-15', '987.654.321-11', 'SP-654322', 'SP', 'Jardins', 'São Paulo', 'Avenida B', 200, 'Apto 101', '01456-789', 'FPJJ12346'),
(3, 3, 'Matheus', 'Lima', '21977778888', 'matheus@email.com', '2010-12-20', '456.789.123-11', 'RJ-789124', 'RJ', 'Copacabana', 'Rio de Janeiro', 'Rua C', 50, NULL, '22040-010', 'FPJJ12347'),
(4, 4, 'Carolina', 'Oliveira', '41966667777', 'carolina@email.com', '2007-06-25', '321.654.987-11', 'PR-321655', 'PR', 'Centro', 'Curitiba', 'Rua D', 75, NULL, '80010-000', 'FPJJ12348'),
(5, 5, 'João', 'Ferreira', '51955556666', 'joao@email.com', '2012-01-30', '111.222.333-55', 'RS-111223', 'RS', 'Moinhos de Vento', 'Porto Alegre', 'Rua E', 30, NULL, '90450-000', 'FPJJ12349'),
(6, 6, 'Isabela', 'Mendes', '48944445555', 'isabela@email.com', '2006-04-05', '555.666.777-99', 'SC-555667', 'SC', 'Centro', 'Florianópolis', 'Rua F', 90, NULL, '88010-400', 'FPJJ12350'),
(7, 7, 'Pedro', 'Santos', '81933334444', 'pedro@email.com', '2009-10-15', '999.888.777-77', 'PE-999889', 'PE', 'Boa Viagem', 'Recife', 'Rua G', 45, NULL, '51020-010', 'FPJJ12351'),
(8, 8, 'Larissa', 'Carvalho', '71922223333', 'larissa@email.com', '2011-05-28', '777.666.555-55', 'BA-777667', 'BA', 'Pituba', 'Salvador', 'Rua H', 55, NULL, '41810-000', 'FPJJ12352'),
(9, 9, 'André', 'Ribeiro', '85911112222', 'andre@email.com', '2005-08-18', '222.333.444-66', 'CE-222334', 'CE', 'Meireles', 'Fortaleza', 'Rua I', 70, NULL, '60165-081', 'FPJJ12353'),
(10, 10, 'Camila', 'Barbosa', '91900002222', 'camila@email.com', '2008-06-22', '333.444.555-77', 'PA-333445', 'PA', 'Centro', 'Belém', 'Rua J', 35, NULL, '66010-000', 'FPJJ12354');



