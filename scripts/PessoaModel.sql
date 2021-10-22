CREATE TABLE PessoaModel (
    Id int IDENTITY(1,1) PRIMARY KEY,
    NomeCompleto text NOT NULL,
    CPF varchar(255) NOT NULL,
    DataNascimento date NOT NULL,
	EnderecoModel_Id int NOT NULL,
	FinanceiroModel_Id int NOT NULL
);

