﻿
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
    Complemento VARCHAR(50) NULL,
    NumeroCasa INT NULL
);

ALTER TABLE TbResponsaveis ADD Complemento VARCHAR(50) NULL;

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


DELETE FROM TbAlunos;
DELETE FROM TbResponsaveis;
DELETE FROM TbPlanos;
SELECT * FROM TbResponsaveis;

-- Inserindo dados na tabela 

INSERT INTO TbPlanos (Nome, QtsDias, Mensalidade) VALUES
('Kids Baby', 2, 130.00),
('Kids Júnior', 2, 130.00),
('Kids 1', 3, 130.00),
('Kids 2', 3, 130.00),
('Kids 1', 5, 150.00),
('Kids 2', 5, 150.00),
('Juvenil', 2, 110.00),
('Adulto', 2, 110.00),
('Juvenil', 3, 130.00),
('Adulto', 3, 130.00),
('Juvenil', 5, 150.00),
('Adulto', 5, 150.00),
('Juvenil', 0, 200.00),
('Adulto', 0, 200.00)

INSERT INTO TbResponsaveis (Nome, Sobrenome, CPF, RG, Telefone, Email, Bairro, CEP, Cidade, Rua, Estado, DataDeNascimento, NumeroCasa, Complemento) VALUES
('Carlos', 'Silva', '12345678900', 'MG123456', '31999998888', 'carlos@email.com', 'Centro', '30123456', 'Belo Horizonte', 'Rua A', 'MG', '1980-05-15', 100, NULL),
('Fernanda', 'Souza', '98765432100', 'SP654321', '11988887777', 'fernanda@email.com', 'Jardins', '01456789', 'São Paulo', 'Avenida B', 'SP', '1985-10-20', 200, 'Apto 101'),
('Ricardo', 'Lima', '45678912300', 'RJ789123', '21977776666', 'ricardo@email.com', 'Copacabana', '22040010', 'Rio de Janeiro', 'Rua C', 'RJ', '1990-02-25', 50, NULL),
('Juliana', 'Oliveira', '32165498700', 'PR321654', '41966665555', 'juliana@email.com', 'Centro', '80010000', 'Curitiba', 'Rua D', 'PR', '1983-07-30', 75, NULL),
('Marcos', 'Ferreira', '11122233344', 'RS111222', '51955554444', 'marcos@email.com', 'Moinhos de Vento', '90450000', 'Porto Alegre', 'Rua E', 'RS', '1975-12-10', 30, 'Casa 2'),
('Paula', 'Mendes', '55566677788', 'SC555666', '48944443333', 'paula@email.com', 'Centro', '88010400', 'Florianópolis', 'Rua F', 'SC', '1992-09-05', 90, NULL),
('Gabriel', 'Santos', '99988877766', 'PE999888', '81933332222', 'gabriel@email.com', 'Boa Viagem', '51020010', 'Recife', 'Rua G', 'PE', '1988-11-15', 45, 'Bloco B'),
('Amanda', 'Carvalho', '77766655544', 'BA777666', '71922221111', 'amanda@email.com', 'Pituba', '41810000', 'Salvador', 'Rua H', 'BA', '1995-04-28', 55, NULL),
('Thiago', 'Ribeiro', '22233344455', 'CE222333', '85911110000', 'thiago@email.com', 'Meireles', '60165081', 'Fortaleza', 'Rua I', 'CE', '1981-08-18', 70, 'Casa 10'),
('Luciana', 'Barbosa', '33344455566', 'PA333444', '91900001111', 'luciana@email.com', 'Centro', '66010000', 'Belém', 'Rua J', 'PA', '1977-06-22', 35, NULL);


-- Inserindo dados na tabela TbAlunos
INSERT INTO TbAlunos (IdPlano, IdResponsavel, Nome, Sobrenome, Telefone, Email, DataNascimento, CPF, RG, Estado, Bairro, Cidade, Rua, NumeroCasa, Complemento, CEP, CarteiraFPJJ) VALUES
(NULL, NULL, 'Lucas', 'Silva', '31999990000', 'lucas@email.com', '2005-03-10', '12345678911', 'MG123457', 'MG', 'Centro', 'Belo Horizonte', 'Rua A', 100, NULL, '30123456', 'FPJJ12345'),
(NULL, NULL, 'Beatriz', 'Souza', '11988889999', 'beatriz@email.com', '2008-07-15', '98765432111', 'SP654322', 'SP', 'Jardins', 'São Paulo', 'Avenida B', 200, 'Apto 101', '01456789', 'FPJJ12346'),
(NULL, NULL, 'Matheus', 'Lima', '21977778888', 'matheus@email.com', '2010-12-20', '45678912311', 'RJ789124', 'RJ', 'Copacabana', 'Rio de Janeiro', 'Rua C', 50, NULL, '22040010', 'FPJJ12347'),
(NULL, NULL, 'Carolina', 'Oliveira', '41966667777', 'carolina@email.com', '2007-06-25', '32165498711', 'PR321655', 'PR', 'Centro', 'Curitiba', 'Rua D', 75, NULL, '80010000', 'FPJJ12348'),
(NULL, NULL, 'João', 'Ferreira', '51955556666', 'joao@email.com', '2012-01-30', '11122233355', 'RS111223', 'RS', 'Moinhos de Vento', 'Porto Alegre', 'Rua E', 30, NULL, '90450000', 'FPJJ12349'),
(NULL, NULL, 'Isabela', 'Mendes', '48944445555', 'isabela@email.com', '2006-04-05', '55566677799', 'SC555667', 'SC', 'Centro', 'Florianópolis', 'Rua F', 90, NULL, '88010400', 'FPJJ12350'),
(NULL, NULL, 'Pedro', 'Santos', '81933334444', 'pedro@email.com', '2009-10-15', '99988877777', 'PE999889', 'PE', 'Boa Viagem', 'Recife', 'Rua G', 45, NULL, '51020010', 'FPJJ12351'),
(NULL, NULL, 'Larissa', 'Carvalho', '71922223333', 'larissa@email.com', '2011-05-28', '77766655555', 'BA777667', 'BA', 'Pituba', 'Salvador', 'Rua H', 55, NULL, '41810000', 'FPJJ12352'),
(NULL, NULL, 'André', 'Ribeiro', '85911112222', 'andre@email.com', '2005-08-18', '22233344466', 'CE222334', 'CE', 'Meireles', 'Fortaleza', 'Rua I', 70, NULL, '60165081', 'FPJJ12353'),
(NULL, NULL, 'Camila', 'Barbosa', '91900002222', 'camila@email.com', '2008-06-22', '33344455577', 'PA333445', 'PA', 'Centro', 'Belém', 'Rua J', 35, NULL, '66010000', 'FPJJ12354');
