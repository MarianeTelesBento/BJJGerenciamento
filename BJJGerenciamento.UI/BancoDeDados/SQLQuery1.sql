CREATE DATABASE BJJ_DB;
GO

USE BJJ_DB;
GO

CREATE TABLE TBAlunos (
    IdAlunos INT IDENTITY(1,1) PRIMARY KEY,
    IdTurma INT NOT NULL,
    Matricula INT,
    Nome VARCHAR(100) NOT NULL,
    Sobrenome VARCHAR(100) NOT NULL,
    EstadoMatricula BIT NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Rg VARCHAR(20) NOT NULL,
    Cpf VARCHAR(14) UNIQUE NOT NULL,
    DataNascimento DATE NOT NULL,
    CEP VARCHAR(10) NOT NULL,
    Endereco VARCHAR(255) NOT NULL,
    Bairro VARCHAR(100) NOT NULL,
    Cidade VARCHAR(100) NOT NULL,
    Estado VARCHAR(100) NOT NULL,
    Numero VARCHAR(10) NOT NULL
);

INSERT INTO TBAlunos (IdTurma, Nome, Sobrenome, EstadoMatricula, Telefone, Email, Rg, Cpf, DataNascimento, CEP, Endereco, Bairro, Cidade, Estado, Numero)
VALUES
(1, 'Lucas', 'Silva', 1, '11987654321', 'lucas.silva@email.com', '12345678-9', '111.222.333-44', '2000-05-15', '01001-000', 'Rua A, 123', 'Centro', 'São Paulo', 'SP', '12A'),
(2, 'Mariana', 'Souza', 1, '21987654322', 'mariana.souza@email.com', '22345678-9', '222.333.444-55', '1998-07-22', '02002-111', 'Rua B, 456', 'Jardins', 'Rio de Janeiro', 'RJ', '34B'),
(3, 'Roberto', 'Lima', 1, '31987654323', 'roberto.lima@email.com', '32345678-9', '333.444.555-66', '1995-09-30', '03003-222', 'Rua C, 789', 'Boa Vista', 'Belo Horizonte', 'MG', '56C'),
(1, 'Ana', 'Pereira', 1, '41987654324', 'ana.pereira@email.com', '42345678-9', '444.555.666-77', '2001-12-10', '04004-333', 'Rua D, 321', 'Vila Nova', 'Curitiba', 'PR', '78D'),
(2, 'Fernando', 'Almeida', 1, '51987654325', 'fernando.almeida@email.com', '52345678-9', '555.666.777-88', '1999-03-05', '05005-444', 'Rua E, 654', 'Bela Vista', 'Porto Alegre', 'RS', '90E'),
(3, 'Juliana', 'Mendes', 1, '61987654326', 'juliana.mendes@email.com', '62345678-9', '666.777.888-99', '2002-08-19', '06006-555', 'Rua F, 987', 'Liberdade', 'Salvador', 'BA', '12F'),
(1, 'Carlos', 'Ferreira', 1, '71987654327', 'carlos.ferreira@email.com', '72345678-9', '777.888.999-00', '1996-11-25', '07007-666', 'Rua G, 741', 'Santa Cecília', 'Fortaleza', 'CE', '34G'),
(2, 'Beatriz', 'Oliveira', 1, '81987654328', 'beatriz.oliveira@email.com', '82345678-9', '888.999.000-11', '1997-06-14', '08008-777', 'Rua H, 852', 'Aclimação', 'Recife', 'PE', '56H'),
(3, 'Gabriel', 'Santos', 1, '91987654329', 'gabriel.santos@email.com', '92345678-9', '999.000.111-22', '1994-04-02', '09009-888', 'Rua I, 963', 'Moema', 'Florianópolis', 'SC', '78I'),
(1, 'Renata', 'Carvalho', 1, '10198765430', 'renata.carvalho@email.com', '102345678-9', '000.111.222-33', '2003-01-28', '10010-999', 'Rua J, 159', 'Ipiranga', 'Brasília', 'DF', '90J');

DROP TABLE TBAlunos;