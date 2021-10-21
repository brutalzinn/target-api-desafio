CREATE TABLE PessoaModel (
    Id int IDENTITY(1,1) PRIMARY KEY,
    NomeCompleto text NOT NULL,
    CPF varchar(255) NOT NULL,
    DataNascimento date NOT NULL,
	EnderecoModel_Id int NOT NULL
);

 CREATE TABLE EnderecoModel (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Logradouro varchar(255) NOT NULL,
    Bairro varchar(255) NOT NULL,
	Cidade varchar(255) NOT NULL,
	UF varchar(255) NOT NULL,
	CEP varchar(255) NOT NULL,
    Complemento varchar(255) NOT NULL
);