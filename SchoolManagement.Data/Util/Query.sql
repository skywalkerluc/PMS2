
SET IDENTITY_INSERT [dbo].[AnoLetivo] ON
INSERT INTO [dbo].[AnoLetivo] ([AnoLetivoId], [QntUnidades], [Ano]) VALUES (1, 4, 2015)
INSERT INTO [dbo].[AnoLetivo] ([AnoLetivoId], [QntUnidades], [Ano]) VALUES (2, 4, 2016)
SET IDENTITY_INSERT [dbo].[AnoLetivo] OFF


SET IDENTITY_INSERT [dbo].[Disciplina] ON
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (1, N'Português')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (2, N'Inglês')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (3, N'Matemática')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (4, N'Biologia')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (5, N'Física')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (6, N'Química')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (7, N'História')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (8, N'Geografia')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (9, N'Sociologia')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (10, N'Filosofia')
INSERT INTO [dbo].[Disciplina] ([DisciplinaId], [NomeDisciplina]) VALUES (11, N'Redação')
SET IDENTITY_INSERT [dbo].[Disciplina] OFF



SET IDENTITY_INSERT [dbo].[Turma] ON
INSERT INTO [dbo].[Turma] ([TurmaId], [Descricao], [HorariosTurmaId], [Vagas], [AnoLetivo_AnoLetivoId]) VALUES (1, N'1º ano', 1, 60, 1)
INSERT INTO [dbo].[Turma] ([TurmaId], [Descricao], [HorariosTurmaId], [Vagas], [AnoLetivo_AnoLetivoId]) VALUES (2, N'2º ano', 1, 60, 1)
INSERT INTO [dbo].[Turma] ([TurmaId], [Descricao], [HorariosTurmaId], [Vagas], [AnoLetivo_AnoLetivoId]) VALUES (3, N'3º ano', 1, 60, 1)
INSERT INTO [dbo].[Turma] ([TurmaId], [Descricao], [HorariosTurmaId], [Vagas], [AnoLetivo_AnoLetivoId]) VALUES (5, N'1º ano', 2, 60, 1)
INSERT INTO [dbo].[Turma] ([TurmaId], [Descricao], [HorariosTurmaId], [Vagas], [AnoLetivo_AnoLetivoId]) VALUES (6, N'2º ano', 2, 60, 1)
INSERT INTO [dbo].[Turma] ([TurmaId], [Descricao], [HorariosTurmaId], [Vagas], [AnoLetivo_AnoLetivoId]) VALUES (7, N'3º ano', 2, 60, 1)
INSERT INTO [dbo].[Turma] ([TurmaId], [Descricao], [HorariosTurmaId], [Vagas], [AnoLetivo_AnoLetivoId]) VALUES (8, N'Pré-vestibular', 2, 50, 1)
INSERT INTO [dbo].[Turma] ([TurmaId], [Descricao], [HorariosTurmaId], [Vagas], [AnoLetivo_AnoLetivoId]) VALUES (9, N'9º ano', 0, 45, 1)
SET IDENTITY_INSERT [dbo].[Turma] OFF




INSERT INTO [dbo].[DisciplinaTurma] ([Disciplina_DisciplinaId], [Turma_TurmaId]) VALUES (1, 1)
INSERT INTO [dbo].[DisciplinaTurma] ([Disciplina_DisciplinaId], [Turma_TurmaId]) VALUES (2, 1)
INSERT INTO [dbo].[DisciplinaTurma] ([Disciplina_DisciplinaId], [Turma_TurmaId]) VALUES (3, 1)
INSERT INTO [dbo].[DisciplinaTurma] ([Disciplina_DisciplinaId], [Turma_TurmaId]) VALUES (4, 1)



SET IDENTITY_INSERT [dbo].[Usuario] ON
INSERT INTO [dbo].[Usuario] ([Id], [Nome], [DataNascimento], [DataCadastro], [Rg], [Cpf], [Nacionalidade], [Naturalidade], [Foto], [Sexo], [Endereco_NomeRua], [Endereco_Numero], [Endereco_Cep], [Endereco_Bairro], [Endereco_Complemento], [Endereco_Cidade], [Endereco_Estado], [Endereco_Pais], [Contato_Email], [Contato_Telefone], [Contato_Celular], [UserLogin], [Senha], [indicadorAcesso]) VALUES (1, N'Lucas', N'2015-10-24 12:00:00', N'2015-10-24 12:00:00', N'88', N'88488', N'4848', N'484848', 8, 8, N'2151', 15151, N'15151', N'51515', N'515', N'151515', N'15151', N'5151', N'151', N'5151515', N'51515', N'kanawagara', N'12345', 6)
INSERT INTO [dbo].[Usuario] ([Id], [Nome], [DataNascimento], [DataCadastro], [Rg], [Cpf], [Nacionalidade], [Naturalidade], [Foto], [Sexo], [Endereco_NomeRua], [Endereco_Numero], [Endereco_Cep], [Endereco_Bairro], [Endereco_Complemento], [Endereco_Cidade], [Endereco_Estado], [Endereco_Pais], [Contato_Email], [Contato_Telefone], [Contato_Celular], [UserLogin], [Senha], [indicadorAcesso]) VALUES (2, N'Professor', N'1990-06-16 00:00:00', N'2015-12-05 00:00:00', N'884848', N'484848', N'84848', N'4848', 1, 1, N'1415', 51515, N'1515', N'151515', N'51515', N'1515', N'1515', N'1515', N'15151', N'151515', N'1515', N'teste', N'12345', 4)

INSERT INTO [dbo].[Usuario] ([Id], [Nome], [DataNascimento], [DataCadastro], [Rg], [Cpf], [Nacionalidade], [Naturalidade], [Foto], [Sexo], [Endereco_NomeRua], [Endereco_Numero], [Endereco_Cep], [Endereco_Bairro], [Endereco_Complemento], [Endereco_Cidade], [Endereco_Estado], [Endereco_Pais], [Contato_Email], [Contato_Telefone], [Contato_Celular], [UserLogin], [Senha], [indicadorAcesso]) VALUES (3, N'Aluno', N'1994-06-16 00:00:00', N'2015-12-05 00:00:00', N'84848', N'4484848454', N'12154', N'54545188', 1, 1, N'8484848', 4848484, N'8485464654', N'486464646', N'465464564', N'5646464', N'4654654', N'6546464', N'464654', N'56454654', N'465465465', N'aluno', N'12345', 2)
SET IDENTITY_INSERT [dbo].[Usuario] OFF

INSERT INTO [dbo].[Aluno] ([Id], [Turma_TurmaId], [Etnia], [Observacoes], [StatusCadastro]) VALUES (3, 1, 1, N'sadsad', 1)


INSERT INTO [dbo].[Funcionario] ([Id], [Evento_EventoId], [Funcao], [PoderAdministrativo]) VALUES (2, NULL, N'Professor', 0)

INSERT INTO [dbo].[Professor] ([Id], [Notificacao_NotificacaoId], [Especialidade]) VALUES (2, NULL, N'Linguas')



INSERT INTO [dbo].[ProfessorTurma] ([Professor_Id], [Turma_TurmaId]) VALUES (2, 1)

INSERT INTO [dbo].[ProfessorDisciplina] ([Professor_Id], [Disciplina_DisciplinaId]) VALUES (2, 1)
INSERT INTO [dbo].[ProfessorDisciplina] ([Professor_Id], [Disciplina_DisciplinaId]) VALUES (2, 2)
INSERT INTO [dbo].[ProfessorDisciplina] ([Professor_Id], [Disciplina_DisciplinaId]) VALUES (2, 3)

