SET IDENTITY_INSERT [dbo].[Usuario] ON
INSERT INTO [dbo].[Usuario] ([Id], [Nome], [DataNascimento], [DataCadastro], [Rg], [Cpf], [Nacionalidade], [Naturalidade], [Foto], [Sexo], [Endereco_NomeRua], [Endereco_Numero], [Endereco_Cep], [Endereco_Bairro], [Endereco_Complemento], [Endereco_Cidade], [Endereco_Estado], [Endereco_Pais], [Contato_Email], [Contato_Telefone], [Contato_Celular], [UserLogin], [Senha], [indicadorAcesso]) VALUES (1, N'Lucas', N'2015-10-24 12:00:00', N'2015-10-24 12:00:00', N'88', N'88488', N'4848', N'484848', 8, 8, N'2151', 15151, N'15151', N'51515', N'515', N'151515', N'15151', N'5151', N'151', N'5151515', N'51515', N'kanawagara', N'12345', 6)
SET IDENTITY_INSERT [dbo].[Usuario] OFF


SET IDENTITY_INSERT [dbo].[AnoLetivo] ON
INSERT INTO [dbo].[AnoLetivo] ([AnoLetivoId], [QntUnidades], [Ano]) VALUES (1, 4, 2015)
INSERT INTO [dbo].[AnoLetivo] ([AnoLetivoId], [QntUnidades], [Ano]) VALUES (2, 4, 2016)
SET IDENTITY_INSERT [dbo].[AnoLetivo] OFF


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



-- CHECK LATER 
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
